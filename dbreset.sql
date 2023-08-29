DELETE FROM dbo.__EFMigrationsHistory;

DROP TABLE dbo.ScoreHoles;
DROP TABLE dbo.Holes;
DROP TABLE dbo.Scores;
DROP TABLE dbo.Courses;

DELETE FROM ScoreHoles;
DELETE FROM Holes;
DELETE FROM Scores;
DELETE FROM Courses;


-- Insert data --
INSERT INTO Courses (Name)
VALUES
    ('East Park');
INSERT INTO Holes (CourseId, HoleNum, Par, Distance)
VALUES
    (1, 1, 3, 169),
    (1, 2, 4, 326),
    (1, 3, 3, 182),
    (1, 4, 3, 168),
    (1, 5, 4, 264),
    (1, 6, 3, 122),
    (1, 7, 3, 156),
    (1, 8, 3, 133),
    (1, 9, 3, 186),
    (1, 10, 3, 174),
    (1, 11, 4, 242),
    (1, 12, 3, 157),
    (1, 13, 3, 190),
    (1, 14, 4, 339),
    (1, 15, 4, 322),
    (1, 16, 3, 194),
    (1, 17, 4, 345),
    (1, 18, 5, 477);
INSERT INTO Scores(CourseId, Player, Date, Notes)
VALUES
	(1, 'Aaron Rader', '2023-08-16', NULL),
	(1, 'Aaron Rader', '2023-08-22', 'Back 9'),
	(1, 'Aaron Rader', '2023-08-27', NULL);
INSERT INTO ScoreHoles(ScoreId, HoleId, Strokes)
VALUES
	(1, 1, 4),
	(1, 2, 7),
	(1, 3, 5),
	(1, 4, 7),
	(1, 5, 5),
	(1, 6, 6),
	(1, 7, 4),
	(1, 8, 5),
	(1, 9, 4),
	(1, 10, 6),
	(1, 11, 7),
	(1, 12, 7),
	(1, 13, 5),
	(1, 14, 12),
	(1, 15, 7),
	(1, 16, 5),
	(1, 17, 7),
	(1, 18, 9);
INSERT INTO ScoreHoles(ScoreId, HoleId, Strokes)
VALUES
	(2, 10, 6),
	(2, 11, 8),
	(2, 12, 5),
	(2, 13, 6),
	(2, 14, 9),
	(2, 15, 7),
	(2, 16, 5),
	(2, 17, 5),
	(2, 18, 10);
INSERT INTO ScoreHoles(ScoreId, HoleId, Strokes)
VALUES
	(3, 1, 7),
	(3, 2, 9),
	(3, 3, 5),
	(3, 4, 7),
	(3, 5, 7),
	(3, 6, 7),
	(3, 7, 6),
	(3, 8, 4),
	(3, 9, 6),
	(3, 10, 7),
	(3, 11, 7),
	(3, 12, 5),
	(3, 13, 7),
	(3, 14, 7),
	(3, 15, 6),
	(3, 16, 7),
	(3, 17, 9),
	(3, 18, 9);

SELECT s.Date, c.Name, s.Notes, h.HoleNum AS 'Hole', sc.Strokes, h.Par, (sc.Strokes - h.Par) AS 'Score'
FROM ScoreHoles sc INNER JOIN Holes h
ON sc.HoleId = h.Id INNER JOIN Courses c
ON h.CourseId = c.Id INNER JOIN Scores s
ON sc.ScoreId = s.Id;