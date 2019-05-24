//namespace VidlyExercice1.Models
//{
//    using System;
//    using System.Data.Entity;
//    using System.Linq;

//    public class Vidly2Context : DbContext
//    {
//        // Votre contexte a été configuré pour utiliser une chaîne de connexion « Vidly2Context » du fichier 
//        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
//        // la base de données « VidlyExercice1.Models.Vidly2Context » sur votre instance LocalDb. 
//        // 
//        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
//        // la chaîne de connexion « Vidly2Context » dans le fichier de configuration de l'application.
//        public Vidly2Context()
//            : base("name=VidlyExo2BDD")
//        {
//        }

//        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
//        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

//        // public virtual DbSet<MyEntity> MyEntities { get; set; }
//        public virtual DbSet<Customer> Customers { get; set; }
//        public virtual DbSet<Movie> Movies { get; set; }
//        public virtual DbSet<MembershipType> MembershipTypes { get; set; }
//        public virtual DbSet<MovieGenre> MovieGenres { get; set; }
//    }

//    //public class MyEntity
//    //{
//    //    public int Id { get; set; }
//    //    public string Name { get; set; }
//    //}
//}