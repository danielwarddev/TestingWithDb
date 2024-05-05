add:
	dotnet ef migrations add $(name) -s ../TestingWithDb.Api --context ProductDbContext

remove:
	dotnet ef migrations remove -s ../TestingWithDb.Api --context ProductDbContext

update-db:
	dotnet ef database update -s ../TestingWithDb.Api --context ProductDbContext

revert-db:
	dotnet ef database update 0 -s ../TestingWithDb.Api --context ProductDbContext
	
revert-db-to:
	dotnet ef database update $(name) -s ../TestingWithDb.Api --context ProductDbContext

script:
	dotnet ef migrations script -s ../TestingWithDb.Api --context ProductDbContext --idempotent -o migration.sql