USE [IHearBanjos];
GO

set identity_insert [UserTypes] on
insert into [UserTypes] ([ID], [Name]) VALUES (1, 'Admin'), (2, 'Author');
set identity_insert [UserTypes] off

set identity_insert [Types] on
insert into [Types] ([Id], [Name]) 
values (1, 'Old Time'), (2, 'Bluegrass'), (3, 'Clawhammer'), (4, 'Two Finger')
set identity_insert [Types] off

set identity_insert [Difficulties] on
insert into [Difficulties] ([Id], [Name]) 
values (1, 'Beginner'), (2, 'Intermediate'), (3, 'Expert')
set identity_insert [Difficulties] off

set identity_insert [Banjoists] on

insert into Banjoists (Id, Name, Email, UserTypeId, FirebaseUserId) values (1, 'Wilson Bell', 'wtbell@mc.edu', 1, 'wdy4jiyf31XG83PTzgezRmJrfoG3');
insert into Banjoists (Id, Name, Email, UserTypeId, FirebaseUserId) values (2, 'Tanner Bell', 'tlbell1@mc.edu', 2, 'xCkxNIXaJihg8zFOeiWHLJZZFmd2');
insert into Banjoists (Id, Name, Email, UserTypeId, FirebaseUserId) values (3, 'Tucker Bell', 'tlbell2@mc.edu', 2, 'VRIBYvjJqqf4EaqEw09YqeaYsro2');
insert into Banjoists (Id, Name, Email, UserTypeId, FirebaseUserId) values (4, 'Wesley Bell', 'wabell@mc.edu', 1, 'QsCciAv3IufW6yL4CWzWHvANHwH2');

set identity_insert [Banjoists] off

set identity_insert [Tabs] on

insert into Tabs (Id, Title, Image, Description, BanjoistId, TypeId, DifficultyId) values (1, 'My Home Is Across The Blue Ridge Mountains', 0x476F76696E646172616A204B616E6E69617070616E0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 'Clawhammer version of tune popularized by Doc Watson.', 1, 3, 1);
insert into Tabs (Id, Title, Image, Description, BanjoistId, TypeId, DifficultyId) values (2, 'The Crawdad Song', 0x476F76696E646172616A204B616E6E69617070616E0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 'You get a line, Ill get a pole.', 2, 3, 1);
insert into Tabs (Id, Title, Image, Description, BanjoistId, TypeId, DifficultyId) values (3, 'Amazing Grace', 0x476F76696E646172616A204B616E6E69617070616E0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 'The popular hymn.', 1, 4, 2);
insert into Tabs (Id, Title, Image, Description, BanjoistId, TypeId, DifficultyId) values (4, 'Midnight Special', 0x476F76696E646172616A204B616E6E69617070616E0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 'Banjo version of Willie Watsons cover.', 3, 1, 2);

set identity_insert [Tabs] off