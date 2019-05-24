namespace VidlyExercice1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePropMovieInClassRental : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rentals", name: "Movies_Id", newName: "Movie_Id");
            RenameIndex(table: "dbo.Rentals", name: "IX_Movies_Id", newName: "IX_Movie_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rentals", name: "IX_Movie_Id", newName: "IX_Movies_Id");
            RenameColumn(table: "dbo.Rentals", name: "Movie_Id", newName: "Movies_Id");
        }
    }
}
