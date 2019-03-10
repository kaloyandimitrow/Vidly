namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
			Sql("INSERT INTO GENRES (NAME) VALUES('Comedy')");
			Sql("INSERT INTO GENRES (NAME) VALUES('Action')");
			Sql("INSERT INTO GENRES (NAME) VALUES('Family')");
			Sql("INSERT INTO GENRES (NAME) VALUES('Romance')");
		}
        
        public override void Down()
        {
        }
    }
}
