## Migrations

### Setup migrations
```powershell
Enable-Migration <name>
```

### Create a new migration file
```powershell
Add-Migration <name>
```

### Update DB from last migration
```powershell
Update-Database
```

### Remove all CASCADE DELETE from all entities

In PiggyContext.cs, add :
```csharp
protected override void OnModelCreating(DbModelBuilder modelBuilder)
{
	base.OnModelCreating(modelBuilder);
	modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
}
```