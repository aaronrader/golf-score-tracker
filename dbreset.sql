DROP TABLE Holes;
DROP TABLE Scores;
DROP TABLE Courses;

CREATE TABLE Courses (
Id INT IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(200) NOT NULL,
Section VARCHAR(50) NOT NULL,
);
CREATE TABLE Holes (
Id INT IDENTITY(1,1) PRIMARY KEY,
CourseId INT NOT NULL FOREIGN KEY REFERENCES dbo.Courses(Id),
Par INT NOT NULL,
Distance INT NOT NULL
);
CREATE TABLE Scores (
Id INT IDENTITY(1,1) PRIMARY KEY,
CourseId INT NOT NULL FOREIGN KEY REFERENCES dbo.Courses(Id),
Player VARCHAR(100) NOT NULL,
Strokes INT NOT NULL,
Date DATE NOT NULL
);

-- Insert data for the "Courses" table
INSERT INTO Courses (Name, Section)
VALUES
    ('East Park', 'Full'),
    ('East Park', 'Back 9');
INSERT INTO Holes (CourseId, Par, Distance)
VALUES
    (1, 3, 169),
    (1, 4, 326),
    (1, 3, 182),
    (1, 3, 168),
    (1, 4, 264),
    (1, 3, 122),
    (1, 3, 156),
    (1, 3, 133),
    (1, 3, 186),
    (1, 3, 174),
    (1, 4, 242),
    (1, 3, 157),
    (1, 3, 190),
    (1, 4, 339),
    (1, 4, 322),
    (1, 3, 194),
    (1, 4, 345),
    (1, 5, 450);
INSERT INTO Holes (CourseId, Par, Distance)
VALUES
    (2, 3, 174),
    (2, 4, 242),
    (2, 3, 157),
    (2, 3, 190),
    (2, 4, 339),
    (2, 4, 322),
    (2, 3, 194),
    (2, 4, 345),
    (2, 5, 477);
INSERT INTO Scores(CourseId, Player, Strokes, Date)
VALUES
	(1, 'Aaron Rader', 112, '2023-08-16'),
	(2, 'Aaron Rader', 61, '2023-08-22'),
	(1, 'Aaron Rader', 122, '2023-08-27')