/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if not exists (select * from dbo.Foods)
begin
    insert into dbo.Foods (Title, [Description], Price)
    values ('Cheeseburger Meal', 'A cheeseburger, fries, and a drink', 5.95),
    ('Chilli dog Meal', 'A chilli dog, fries, and a drink', 4.15),
    ('Vegan Meal', 'A large salad and a water', 1.95);
end