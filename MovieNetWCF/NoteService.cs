﻿using MovieNetLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MovieNetWCF
{
	// REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class NoteService : INoteService
	{
		public Note GetNote(int id)
		{
            NoteEntity notesEntity = null;

            try
            {
                DataModelContainer ctx = new DataModelContainer();
                notesEntity = (from m in ctx.NoteEntitySet where m.Id == id select m).FirstOrDefault();
                if (notesEntity == null)
                    return null;
            }
            catch (SqlException ex)
            {
                throw new FaultException("Erreur GetNote: " + ex.Errors);
            }

            return TranslateNote(notesEntity);
        }
		public Note CreateNote(int rating, string comment, int idUser, int idMovie)
		{
            NoteEntity note = null;

            try
            {
                DataModelContainer ctx = new DataModelContainer();
                MovieService ms = new MovieService();
                UserService us = new UserService();
                var msq = (from m in ctx.MovieEntitySet where m.Id == idMovie select m).FirstOrDefault();
                var usq = (from u in ctx.UserEntitySet where u.Id == idUser select u).FirstOrDefault();
                note = new NoteEntity
                {
                    Rating = rating,
                    Comment = comment,
                    MovieEntity = msq,
                    UserEntity = usq
                };

                ctx.NoteEntitySet.Add(note);
                ctx.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new FaultException("Erreur CreateNote: " + ex.Errors);
            }

			return TranslateNote(note);
		}

		public Note UpdateNote(int id, int rating, string comment)
		{
            NoteEntity notesEntity = null;

            try
            {
                DataModelContainer ctx = new DataModelContainer();
                notesEntity = (from m in ctx.NoteEntitySet where m.Id == id select m).FirstOrDefault();
                if (notesEntity == null)
                    return null;

                notesEntity.Rating = rating;
				notesEntity.Comment = comment;
                ctx.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new FaultException("Erreur UpdateNote: " + ex.Errors);
            }

			return TranslateNote(notesEntity);
		}

		public Boolean DeleteNote(int id)
		{
			NoteEntity notesEntity = null;
			try
            {
                DataModelContainer ctx = new DataModelContainer();

                notesEntity = (from m in ctx.NoteEntitySet where m.Id == id select m).FirstOrDefault();
                if (notesEntity == null)
                    return false;

                ctx.NoteEntitySet.Remove(notesEntity);
                ctx.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new FaultException("Erreur DeleteNote: " + ex.Errors);
            }

			return true;
		}

		public List<Note> FindNotesOfUser(int idUser)
		{
			List<Note> notes = new List<Note>();

			try
			{
				DataModelContainer ctx = new DataModelContainer();

				var query = (from n in ctx.NoteEntitySet where n.UserEntity.Id == idUser select n).ToList();
				List<NoteEntity> list = query.ToList();

				foreach (var note in list)
				{
					notes.Add(TranslateNote(note));
				}
			}
			catch (SqlException ex)
			{
				throw new FaultException("Erreur Finding note of user " + ex.Errors);
			}

			return notes;
		}

		public List<Note> FindNotesOfMovie(int idMovie)
		{
            List<Note> notes = new List<Note>();

            try
            {
                DataModelContainer ctx = new DataModelContainer();

                var query = (from n in ctx.NoteEntitySet where n.MovieEntity.Id == idMovie select n).ToList();
                List<NoteEntity> list = query.ToList();

                foreach (var note in list)
                {
                    notes.Add(TranslateNote(note));
                }
            }
            catch (SqlException ex)
            {
                throw new FaultException("Erreur Finding note of movie " + ex.Errors);
            }

			return notes;
		}

		private Note TranslateNote(NoteEntity notesEntity)
		{
			MovieService ms = new MovieService();

			Note note = new Note
			{
				Id = notesEntity.Id,
				Rating = notesEntity.Rating,
				Comment = notesEntity.Comment,
				MovieEntity_Id = notesEntity.MovieEntity.Id,
				UserEntity_Id = notesEntity.UserEntity.Id
			};
			return note;
		}

		internal NoteEntity ReverseTranslateNote(Note note)
		{
			DataModelContainer ctx = new DataModelContainer();

			MovieService ms = new MovieService();
			UserService us = new UserService();

			var usq = (from u in ctx.UserEntitySet where u.Id == note.UserEntity_Id select u).FirstOrDefault();
			var msq = (from m in ctx.MovieEntitySet where m.Id == note.MovieEntity_Id select m).FirstOrDefault();
			NoteEntity noteEntity = new NoteEntity
			{
				Id = note.Id,
				Comment = note.Comment,
				Rating = note.Rating,
				UserEntity = usq,
				MovieEntity = msq
			};
			return noteEntity;
		}

	}
}
