﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MovieNetLibrary;
namespace MovieNetWCF
{
	// REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
	[ServiceContract]
	public interface IMovieService
	{
		[OperationContract]
		Movie GetMovie(int id);

		[OperationContract]
		Movie CreateMovie(string title, string genre, string summary);

		[OperationContract]
		Movie UpdateMovie(int id, string title, string genre, string summary);

		[OperationContract]
		Boolean DeleteMovie(int id);

		[OperationContract]
		List<Movie> FindMovieTitle(string title);

        [OperationContract]
        List<Movie> FindMovieGenre(string genre);

        [OperationContract]
		List<Movie> GetAllMovies();
		// TODO: ajoutez vos opérations de service ici
	}

	// Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
	// Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "MovieNetWCF.ContractType".
	[DataContract]
	public class Movie
	{
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string Title { get; set; }
		[DataMember]
		public string Genre { get; set; }
		[DataMember]
		public string Summary { get; set; }
	}
}
