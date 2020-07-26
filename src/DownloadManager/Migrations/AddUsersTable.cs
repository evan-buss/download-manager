using FluentMigrator;

namespace DownloadManager.Migrations
{
    [Migration(1)]
    public class AddUsersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("Salt").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}