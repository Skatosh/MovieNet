﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MovieNetWPF.WCFMovieService;
using MovieNetWPF.WCFNoteService;
using MovieNetWPF.WCFUserService;

namespace MovieNetWPF
{
	public class MainViewModel : ViewModelBase
	{
		private MovieServiceClient msc = new MovieServiceClient();
		private NoteServiceClient nsc = new NoteServiceClient();
		private UserServiceClient usc = new UserServiceClient();
		private LoginWindow loginView;
		private MainWindow mainView;
		private Window currentView;
		public MainViewModel()
		{
			RefreshMovies(); // init of movies list
			UpdateMovie = new RelayCommand(UpdateMovieExecute, UpdateMovieCanExecute); // Binding update movie to button
			UpdateNote = new RelayCommand(UpdateNoteExecute, UpdateNoteCanExecute); // binding update note to button 
			UpdateUser = new RelayCommand(UpdateUserExecute); // Binding update movie to button
			DelMovie = new RelayCommand(DelMovieExecute, DelMovieCanExecute); // binding to add note button
			DelNote = new RelayCommand<CustNote>(DelNoteExecute);
			Login = new RelayCommand<object>(LoginExecute, LoginCanExecute); // binding to login
			MyProfile = new RelayCommand(MyProfileExecute); // binding to myprofile
			Disconnect = new RelayCommand(DisconnectExecute); // binding to disconnect
			Register = new RelayCommand<object>(RegisterExecute, RegisterCanExecute); // binding to register
			ClosingCommand = new RelayCommand<CancelEventArgs>(ClosingWindow); // register closing event
			NoteGen = new RelayCommand(NoteGenExecute, NoteGenCanExecute); // Movie gen button of mainview (button add new note)
			MovieGen = new RelayCommand(MovieGenExecute); // Movie gen button of mainview 
			DataButton = new RelayCommand(DataButtonExecute, DataButtonCanExecute); // DataButton of DataGenerator view
		}

		// CurrentMovie prop
		private Movie currentMovie;
		public Movie CurrentMovie
		{
			get
			{
				return currentMovie;
			}
			set
			{
				currentMovie = value;
				RaisePropertyChanged();
				RefreshNotes();
			}
		}

		// the filter for the ComboBox : title has tocontain the typed value else if empty reset CurrentMovie
		private String comboTextFilter;
		public String ComboTextFilter
		{
			get { return comboTextFilter; }
			set
			{
				comboTextFilter = value;
				var tmp = new List<Movie>();
				if (currentMovie != null)
					if (CurrentMovie.Title == value && filteredMovies != movies)
					{
						FilteredMovies = movies;
						return;
					}
				if (value != "")
				{
					foreach (var f in movies)
					{
						if (f.Title.ToLower().Contains(value.ToLower()))
							tmp.Add(f);
					}
					FilteredMovies = tmp;
				}
				else
				{
					Tips = "Tips : No movie selected ? \nCreate one or use the combobox to search/select one";
					FilteredMovies = movies;
					CurrentMovie = null;
					Notes = new List<CustNote>();
				}
			}
		}
		// prop used for comboboxTextFilter is a list of movies from original and filter is applied to it
		private List<Movie> filteredMovies;
		public List<Movie> FilteredMovies
		{
			get { return filteredMovies; }
			set
			{
				filteredMovies = value;
				RaisePropertyChanged();
			}
		}
		// original list of all movies
		private List<Movie> movies;
		public List<Movie> Movies
		{
			get
			{
				return movies;
			}
			set
			{
				movies = value;
				RaisePropertyChanged();
			}
		}
		// refresh of list of movies
		private void RefreshMovies()
		{
			FilteredMovies = Movies = msc.GetAllMoviesAsync().Result.ToList();
		}
		public RelayCommand UpdateMovie { get; }
		
		// Update movie with current movie data
		void UpdateMovieExecute()
		{
			if (msc.UpdateMovieAsync(this.CurrentMovie.Id, this.CurrentMovie.Title, this.CurrentMovie.Genre, this.CurrentMovie.Summary) != null)
				MessageBox.Show("Movie has been updated");
			else
				MessageBox.Show("An error has occured please contact administrator");
		}
		// check if current movie is null
		bool UpdateMovieCanExecute()
		{
			return currentMovie != null;
		}
		// property current user has login and password, only owner can modify note
		private User currentUser;
		public User CurrentUser
		{
			get
			{
				return currentUser;
			}
			set
			{
				currentUser = value;
				RaisePropertyChanged();
			}
		}
		// prop on the left side of login page 
		private String logLogin;
		public String LogLogin
		{
			get
			{
				 return logLogin;
			}
			set
			{
				logLogin = value;
				RaisePropertyChanged();
			}
		}

		public RelayCommand<object> Login { get; }

		// when button log in is pressed register the login view and hide it, open mainview and set the datacontext
		void LoginExecute(object param)
		{
			Tips = "Tips : No movie selected ? \nCreate one or use the combobox to search/select one";
			var passwordBox = param as PasswordBox;
			var LogPassword = passwordBox.Password;
			if ((currentUser = usc.LoginUser(LogLogin, LogPassword)) == null)
				MessageBox.Show("Invalid User and Password");
			else
			{
				if (CurrentMovie != null)
				{
					CurrentMovie = new Movie();
					ComboTextFilter = String.Empty;
				}
				mainView = new MainWindow
				{
					DataContext = this,
				};
				mainView.Show();
				if (loginView != null)
					loginView.Close();
				else
					App.Current.MainWindow.Close();
			}
		}
		// check if left side login is null
		private bool LoginCanExecute(object param)
		{
			return LogLogin != null;
		}
		//prop on right side register login
		private String regLogin;
		public String RegLogin
		{
			get
			{
				return regLogin;
			}
			set
			{
				regLogin = value;
				RaisePropertyChanged();
			}
		}

		public RelayCommand<object> Register { get; }
		// create a new user and check if login and password is filled or a msg box will pop
		void RegisterExecute(object param)
		{
			var passwordBox = param as PasswordBox;
			var RegPassword = passwordBox.Password;
			if (RegPassword != "")
			{
				User user = usc.CreateUser(RegLogin, RegPassword);
				if (user != null)
				{
					passwordBox.Password = String.Empty;
					RegLogin = String.Empty;
					MessageBox.Show("Your user has been created");
				}
				else
					MessageBox.Show("User already exist");
			}
			else
				MessageBox.Show("Both login and password has to be filled");
		}
		// Check if login is null
		bool RegisterCanExecute(object param)
		{
			return RegLogin != null;
		}
		// list of note to be showed for the movies 
		private List<CustNote> notes;
		public List<CustNote> Notes
		{
			get
			{
				return notes;
			}
			set
			{
				notes = value;
				RaisePropertyChanged();
			}
		}
		// prop currently selected note
		private CustNote currentNote;
		public CustNote CurrentNote
		{
			get
			{
				return currentNote;
			}
			set
			{
				currentNote = value;
				Tips = "Tips : only note creator can modify their rating.\n1 rating per user";
				if (currentNote != null)
					CheckIfCreator();
				RaisePropertyChanged();
			}
		}
		private bool noteUpdateReadOnly;
		public bool NoteUpdateReadOnly
		{
			get
			{
				return noteUpdateReadOnly;
			}
			set
			{
				noteUpdateReadOnly = value;
				RaisePropertyChanged();
			}
		}
		// check if currently logged user is the creator of the note so he can modify or not
		private bool CheckIfCreator()
		{
			var mynote = nsc.GetNote(currentNote.Id);
			if (mynote.UserEntity_Id == currentUser.Id)
			{
				NoteUpdateReadOnly = false;
				return true;
			}
			NoteUpdateReadOnly = true;
			return false;
		}

		public RelayCommand UpdateUser { get; }
		private void UpdateUserExecute()
		{
			if (usc.UpdateUserAsync(currentUser.Id, currentUser.Login, currentUser.Password) != null)
				MessageBox.Show("User succesfully updated");
			else
				MessageBox.Show("Update has failed, please contact an administrator");
		}


		public RelayCommand UpdateNote { get; }

		//update the current note
		private void UpdateNoteExecute()
		{
			if (!Enumerable.Range(0, 21).Contains(currentNote.Rating))
			{
				RefreshNotes();
				MessageBox.Show("Rating is not within 0 and 20");

			}
			else
			{
				nsc.UpdateNoteAsync(currentNote.Id, currentNote.Rating, currentNote.Comment);
				MessageBox.Show("Note has been updated");
			}
		}
		// see if is creator and a note is selected
		private bool UpdateNoteCanExecute()
		{
			return currentNote != null && CheckIfCreator();
		}

		public RelayCommand Disconnect { get; }
		// close the windows
		private void DisconnectExecute()
		{
			mainView.Close();
		}
		// prop noteAverage contain the average of notes
		private String noteAverage;
		public String NoteAverage
		{
			get
			{
				return noteAverage;
			}
			set
			{
				noteAverage = value;
				RaisePropertyChanged();
			}
		}

		public RelayCommand DelMovie { get; }
		// see if current modify is selected
		private bool DelMovieCanExecute()
		{
			return currentMovie != null;
		}
		// delete movie, has a confirmation window , will delete related note
		private void DelMovieExecute()
		{
			var dialogResult = MessageBox.Show("This will delete movie and all related rating", "Confirmation window", MessageBoxButton.YesNo);
			if (dialogResult == MessageBoxResult.Yes)
			{
				msc.DeleteMovieAsync(currentMovie.Id);
				RefreshMovies();
			}
		}
		// Refresh the note of current movie, calculate the noteAverage, is called when CurrentMovie setter is called.
		private void RefreshNotes()
		{
			if (currentMovie != null)
			{
				List<CustNote> notes = new List<CustNote>();
				foreach (var n in nsc.FindNotesOfMovieAsync(CurrentMovie.Id).Result.ToList())
				{
					notes.Add(new CustNote() { Rating = n.Rating, Comment = n.Comment, UserId = n.UserEntity_Id, Id = n.Id });
				}
				if (notes.Count > 0)
				{
					NoteAverage = String.Format("{0:0.0}", notes.Average(x => x.Rating)) + " / 20 out of " + notes.Count() + " notes";
					foreach (var u in notes)
					{
						u.User = usc.GetUser(u.UserId).Login;
					}
				}
				else
					NoteAverage = "No rating yet, be the first to rate this movie !";
				Notes = notes;
				Tips = "Tips : You can add rating to movies. \nAll your rating will be shown in \"My Profile\"";
			}
			else
				NoteAverage = "";
		}

		public RelayCommand<CancelEventArgs> ClosingCommand { get; private set; }
		// when main view is closed actions show previous login and remove the datas.
		private void ClosingWindow(CancelEventArgs obj)
		{
			loginView = new LoginWindow();
			loginView.Show();
			ResetLoginDatas();
		}

		//Remove login datas 
		private void ResetLoginDatas()
		{
			LogLogin = String.Empty;
			RegLogin = String.Empty;
		}

		// string for the button on dataCreation window
		private String buttonContent;
		public String ButtonContent 
		{
			get
			{
				return buttonContent;
			}
			set
			{
				buttonContent = value;
				RaisePropertyChanged();
			}
		}

		public RelayCommand NoteGen { get; }
		// is used when add note is used. This will set the textbox and textblock values 
		private void NoteGenExecute()
		{
			currentView = new DataCreation
			{
				DataContext = this,
			};
			ButtonContent = "Create Note";
			TBlock = "Comment";
			TBlock1 = "Rating";
			TBox2Visibility = "Hidden";
			TBox2ratingVisibility = "Visible";
			TBlock2 = String.Empty;
			RaisePropertyChanged();
			currentView.ShowDialog();
		}
		// check if a movie is selected
		private bool NoteGenCanExecute()
		{
			return currentMovie != null && !CheckHasRated();
		}

		private bool CheckHasRated()
		{
			var match = notes.Where(s => s.User.Contains(currentUser.Login)).FirstOrDefault();
			if (match == null)
				return false;
			return true;
		}

		public RelayCommand MovieGen { get; }
		// is used when createmovie is used, this will set the textbox and textblock values.
		private void MovieGenExecute()
		{
			currentView = new DataCreation
			{
				DataContext = this
			};
			ButtonContent = "Create Movie";
			TBlock = "Title";
			TBlock1 = "Genre";
			TBlock2 = "Summary";
			TBox2Visibility = "Visible";
			TBox2ratingVisibility = "Hidden";
			RaisePropertyChanged();
			currentView.ShowDialog();
		}
		//prop for visibility for 2nd row textbox 
		public String TBox2Visibility{ get; set; }
		public String TBox2ratingVisibility { get; set; }

		public RelayCommand DataButton { get; }
		// the button in datacreation different execution for different buttonContent value
		private void DataButtonExecute()
		{
			if (buttonContent == "Create Movie" && !String.IsNullOrEmpty(TBox2))
			{
				msc.CreateMovie(TBox, TBox1, TBox2);
				RefreshMovies();
				CurrentMovie = movies.Last();
			}
			else if (buttonContent == "Create Note")
			{
				if (!Enumerable.Range(0, 21).Contains(TBox2rating))
				{
					MessageBox.Show("Rating must be within 0 and 20.");
					return;
				}
				nsc.CreateNote(TBox2rating, TBox, currentUser.Id, currentMovie.Id);
				RefreshNotes();
			}
			else
			{
				MessageBox.Show("You have to fill all the information.");
				return;
			}
			TBox = String.Empty;
			TBox1 = String.Empty;
			TBox2 = String.Empty;
			TBox2rating = 0;
			currentView.Close();
		}
		//check if we can use button
		private bool DataButtonCanExecute()
		{
			if (buttonContent == "Create Movie")
			{
				return TBox != null && TBox1 != null;
			}

			if (buttonContent == "Create Note")
			{
				return TBox != null;
			}
			return false;
		}


		public RelayCommand MyProfile { get; }

		private void MyProfileExecute()
		{
			Window profileWin = new MyProfile
			{
				DataContext = this
			};
			MyNotes = MyProfileNotes();
			profileWin.ShowDialog();
		}

		private List<CustNote> MyProfileNotes()
		{
			List<CustNote> tmp = new List<CustNote>();
			List<Note> n = nsc.FindNotesOfUserAsync(currentUser.Id).Result.ToList();
			foreach (var m in n)
			{
				tmp.Add(new CustNote() { Movie = msc.GetMovieAsync(m.MovieEntity_Id).Result.Title, Rating = m.Rating, Comment = m.Comment, Id = m.Id });
			}
			return tmp;
		}

		private List<CustNote> myNotes;

		public  List<CustNote> MyNotes
		{
			get { return myNotes; }
			set
			{
				myNotes = value;
				RaisePropertyChanged("MyNotes");
			}
		}

		public RelayCommand<CustNote> DelNote { get; }
		// delete note, has a confirmation window
		private void DelNoteExecute(CustNote param)
		{
			var dialogResult = MessageBox.Show("This will delete the rating", "Confirmation window", MessageBoxButton.YesNo);
			if (dialogResult == MessageBoxResult.Yes)
			{
				if (nsc.DeleteNoteAsync(param.Id).Result)
					MessageBox.Show("Rating has been sucessfully deleted");
				else
					MessageBox.Show("An error has occured contact an administrator");
				MyNotes = MyProfileNotes();
			}
		}

		private String tips;

		public String Tips
		{
			get { return tips; }
			set
			{
				tips = value;
				RaisePropertyChanged();
			}
		}
		// prop for dataCreation tbox
		public String TBox { get; set; }
		public String TBox1 { get; set; }
		public String TBox2 { get; set; }
		public int TBox2rating { get; set; }
		// prop for dataCreation tblock
		public String TBlock { get; set; }
		public String TBlock1 { get; set; }
		public String TBlock2 { get; set; }


		// custom note model, is implemented because datagrid item source cannot be from 2 sources so we gather 2 in 1.
		// Id rating comment are from note
		// User , UserId are from user
		public class CustNote
		{
			public int Id { get; set; }
			public int  Rating{ get; set; }
			public String Comment{ get; set; }
			public String User{ get; set; }
			public int UserId { get; set; }
			public String Movie { get; set; }
		}
	}
}
