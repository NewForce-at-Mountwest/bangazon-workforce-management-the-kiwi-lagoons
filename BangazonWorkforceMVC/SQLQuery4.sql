SELECT e.Id,
                    e.FirstName,
                    e.LastName,
                    e.isSuperVisor,
                    d.Name,
                    p.Manufacturer,
                    p.Make,
                    r.Name AS 'Training Name'

                    FROM Employee e

                    LEFT JOIN Department d
                    ON e.DepartmentId = d.Id

                    LEFT JOIN ComputerEmployee c
                    ON e.Id = c.EmployeeId

                    LEFT JOIN Computer p
                    ON c.ComputerId = p.Id

                    LEFT JOIN EmployeeTraining t
                    ON e.Id = t.EmployeeId

                    LEFT JOIN TrainingProgram r
                    ON t.TrainingProgramId = r.Id

                    WHERE e.id = 8

                    AND c.UnassignDate is null

--IS NULL

--SELECT * FROM Employee


--INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Mace', 'Windu', 4, 'TRUE')
--INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Ahsoka', 'Tano', 1, 'FALSE')
--INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Sheev', 'Palpatine', 3, 'TRUE')
--INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Boba', 'Fett', 3, 'FALSE')
--INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Lando', 'Calrissian', 2, 'FALSE')
--INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Qui-Gon', 'Jinn', 1, 'FALSE')


--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (2,3)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (3,1)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (4,1)
--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (4,2)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (5,1)
--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (5,4)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (6,3)
--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (6,2)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (7,1)
--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (7,2)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (8,4)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (9,1)
--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (9,3)
--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (9,2)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (10,4)
--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (10,2)

--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (5, 2, '02/15/2020')
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (6, 4, '03/15/2020')
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (7, 2, '04/15/2020')
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (8, 3, '06/15/2020')
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (9, 1, '08/15/2020')
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (10, 1, '08/15/2020')
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (5, 1, '01/15/2019', '12/15/2019')
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (6, 3, '01/15/2019', '12/15/2019')
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (7, 2, '04/15/2019', '12/15/2019')
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (8, 4, '05/15/2019', '12/15/2019')
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (9, 3, '04/15/2019', '12/15/2019')
--INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (10, 3, '05/15/2019', '12/15/2019')
