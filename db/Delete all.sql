DELETE FROM [piggybank].[Identity].[ShoppingCartItems];
DELETE FROM [piggybank].[Identity].[ShoppingCarts];
DELETE FROM [piggybank].[Identity].[OrderDetails];
DELETE FROM [piggybank].[Identity].[Orders];
DELETE FROM [piggybank].[Identity].[Reviews];
DELETE FROM [piggybank].[Identity].[Products];
DELETE FROM [piggybank].[Identity].[Manufacturers];
DELETE FROM [piggybank].[Identity].[UserRoles];
DELETE FROM [piggybank].[Identity].[RoleClaims];
DELETE FROM [piggybank].[Identity].[UserClaims];
DELETE FROM [piggybank].[Identity].[UserLogins];
DELETE FROM [piggybank].[Identity].[Role];
DELETE FROM [piggybank].[Identity].[UserTokens];
DELETE FROM [piggybank].[Identity].[User];
DELETE FROM [piggybank].[Identity].[AspNetUsers];
DELETE FROM [piggybank].[Identity].[Addresses];


DBCC CHECKIDENT ('[piggybank].[Identity].[Manufacturers]', RESEED, 0);
DBCC CHECKIDENT ('[piggybank].[Identity].[OrderDetails]', RESEED, 0);
DBCC CHECKIDENT ('[piggybank].[Identity].[Orders]', RESEED, 0);
DBCC CHECKIDENT ('[piggybank].[Identity].[Products]', RESEED, 0);
DBCC CHECKIDENT ('[piggybank].[Identity].[Reviews]', RESEED, 0);
DBCC CHECKIDENT ('[piggybank].[Identity].[Addresses]', RESEED, 0);
DBCC CHECKIDENT ('[piggybank].[Identity].[ShoppingCarts]', RESEED, 0);
DBCC CHECKIDENT ('[piggybank].[Identity].[ShoppingCartItems]', RESEED, 0);
