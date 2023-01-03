
To migrate db use this for user identities
add-migration 'NameYourMigration' -ConfigurationTypeName TheGame.Migrations.Configuration
update-database -ConfigurationTypeName TheGame.Migrations.Configuration

https://jwt.ms/