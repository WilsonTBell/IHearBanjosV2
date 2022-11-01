                        SELECT  t.Id, t.Title, t.TypeId, t.BanjoistId,
                                t.Description, t.Image,
                                b.Name, b.UserTypeId, b.FirebaseUserId,
                                b.Email,
                                ty.Name AS Type, d.Name AS Difficulty, ut.Name as UserType
                        FROM Tabs t
                        LEFT JOIN Banjoists b ON t.BanjoistId = b.Id
                        LEFT JOIN Types ty ON t.TypeId = ty.Id
                        LEFT JOIN Difficulties d ON t.DifficultyId = d.Id
                        LEFT JOIN UserTypes ut ON b.UserTypeId = ut.Id

