namespace WebGitTrending.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Novo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        RootId = c.Int(nullable: false, identity: true),
                        id = c.Int(nullable: false),
                        name = c.String(),
                        full_name = c.String(),
                        _private = c.Boolean(name: "private", nullable: false),
                        html_url = c.String(),
                        description = c.String(),
                        fork = c.Boolean(nullable: false),
                        url = c.String(),
                        forks_url = c.String(),
                        keys_url = c.String(),
                        collaborators_url = c.String(),
                        teams_url = c.String(),
                        hooks_url = c.String(),
                        issue_events_url = c.String(),
                        events_url = c.String(),
                        assignees_url = c.String(),
                        branches_url = c.String(),
                        tags_url = c.String(),
                        blobs_url = c.String(),
                        git_tags_url = c.String(),
                        git_refs_url = c.String(),
                        trees_url = c.String(),
                        statuses_url = c.String(),
                        languages_url = c.String(),
                        stargazers_url = c.String(),
                        contributors_url = c.String(),
                        subscribers_url = c.String(),
                        subscription_url = c.String(),
                        commits_url = c.String(),
                        git_commits_url = c.String(),
                        comments_url = c.String(),
                        issue_comment_url = c.String(),
                        contents_url = c.String(),
                        compare_url = c.String(),
                        merges_url = c.String(),
                        archive_url = c.String(),
                        downloads_url = c.String(),
                        issues_url = c.String(),
                        pulls_url = c.String(),
                        milestones_url = c.String(),
                        notifications_url = c.String(),
                        labels_url = c.String(),
                        releases_url = c.String(),
                        deployments_url = c.String(),
                        created_at = c.String(),
                        updated_at = c.String(),
                        pushed_at = c.String(),
                        git_url = c.String(),
                        ssh_url = c.String(),
                        clone_url = c.String(),
                        svn_url = c.String(),
                        homepage = c.String(),
                        size = c.Int(nullable: false),
                        stargazers_count = c.Int(nullable: false),
                        watchers_count = c.Int(nullable: false),
                        language = c.String(),
                        has_issues = c.Boolean(nullable: false),
                        has_projects = c.Boolean(nullable: false),
                        has_downloads = c.Boolean(nullable: false),
                        has_wiki = c.Boolean(nullable: false),
                        has_pages = c.Boolean(nullable: false),
                        forks_count = c.Int(nullable: false),
                        open_issues_count = c.Int(nullable: false),
                        forks = c.Int(nullable: false),
                        open_issues = c.Int(nullable: false),
                        watchers = c.Int(nullable: false),
                        default_branch = c.String(),
                        score = c.Double(nullable: false),
                        RootObject_RootId = c.Int(),
                    })
                .PrimaryKey(t => t.RootId)
                .ForeignKey("dbo.Owners", t => t.id, cascadeDelete: true)
                .ForeignKey("dbo.RootObjects", t => t.RootObject_RootId)
                .Index(t => t.id)
                .Index(t => t.RootObject_RootId);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        login = c.String(),
                        avatar_url = c.String(),
                        gravatar_id = c.String(),
                        url = c.String(),
                        html_url = c.String(),
                        followers_url = c.String(),
                        following_url = c.String(),
                        gists_url = c.String(),
                        starred_url = c.String(),
                        subscriptions_url = c.String(),
                        organizations_url = c.String(),
                        repos_url = c.String(),
                        events_url = c.String(),
                        received_events_url = c.String(),
                        type = c.String(),
                        site_admin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.RootObjects",
                c => new
                    {
                        RootId = c.Int(nullable: false, identity: true),
                        total_count = c.Int(nullable: false),
                        incomplete_results = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RootId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "RootObject_RootId", "dbo.RootObjects");
            DropForeignKey("dbo.Items", "id", "dbo.Owners");
            DropIndex("dbo.Items", new[] { "RootObject_RootId" });
            DropIndex("dbo.Items", new[] { "id" });
            DropTable("dbo.RootObjects");
            DropTable("dbo.Owners");
            DropTable("dbo.Items");
        }
    }
}
