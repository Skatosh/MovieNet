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
	public interface INoteService
	{
		[OperationContract]
		Note GetNote(int id);

		[OperationContract]
		Note CreateNote(int note, string comment, int idUser, int idMovie);

		[OperationContract]
		Note UpdateNote(int id,int rating, string comment);

		[OperationContract]
		Boolean DeleteNote(int id);

		[OperationContract]
		List<Note> FindNotesOfMovie(int idMovie);
		[OperationContract]
		List<Note> FindNotesOfUser(int idUser);
		// TODO: ajoutez vos opérations de service ici
	}

	// Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
	// Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "MovieNetWCF.ContractType".
	[DataContract]
	public class Note
	{
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public int Rating { get; set; }
		[DataMember]
		public string Comment { get; set; }
		[DataMember]
		public int MovieEntity_Id { get; set; }
		[DataMember]
		public int UserEntity_Id { get; set; }
	}
}
