CREATE TABLE Cities(
`Id` INT PRIMARY KEY AUTO_INCREMENT,
`Name` NVARCHAR(50) NOT NULL
);

INSERT INTO Cinemas(Name, City, Auditorium, CreatedAt)
VALUES ('Forum Cinemas Kaunas', 'Kaunas', 'iScape', '2023-12-28');
DROP TABLE Cinemas;
SELECT * FROM Cinemas;

CREATE TABLE Screenings(
`Id` INT PRIMARY KEY AUTO_INCREMENT,
`CinemaId` INT NOT NULL,
`MovieId` INT NOT NULL,
`ShowDate` DATETIME NOT NULL,
`ShowTime` DATETIME NOT NULL,
`CreatedAt` DATETIME NOT NULL,
FOREIGN KEY (CinemaId) REFERENCES Cinemas(Id),
FOREIGN KEY (MovieId) REFERENCES Movies(Id)
);

INSERT INTO Screenings(CinemaId, MovieId, ShowDate, ShowTime, CreatedAt)
VALUES ('2', '1', '2023-12-28', '17:00:00', '2023-12-28 17:00');

select * From screenings;
SELECT * FROM Auditoriums;

CREATE TABLE Cinemas(
`Id` INT PRIMARY KEY AUTO_INCREMENT,
`Name` NVARCHAR(50),
`CityId` INT NOT NULL,
`AuditoriumId` INT NOT NULL,
`CreatedAt` DATETIME NOT NULL,
FOREIGN KEY (CityId) REFERENCES Cities(Id),
FOREIGN KEY (AuditoriumId) REFERENCES Auditoriums(Id)
);

ALTER TABLE Cinemas
ADD COLUMN MovieId INT;

ALTER TABLE Screenings
DROP COLUMN ShowTime,
DROP COLUMN ShowDate;

ALTER TABLE Cinemas
ADD FOREIGN KEY (MovieId) REFERENCES movies(Id);

ALTER TABLE Screenings DROP FOREIGN KEY cinemas_ibfk_2;

ALTER TABLE Cinemas DROP FOREIGN KEY cinemas_ibfk_1;
ALTER TABLE Cinemas DROP COLUMN MovieId;
DROP TABLE Auditoriums;

SELECT
    TABLE_NAME,
    CONSTRAINT_NAME 
FROM
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE
    REFERENCED_TABLE_NAME = 'Cities'
    AND TABLE_NAME = 'Cinemas';

INSERT INTO Cinemas(
Name,
CityId,
AuditoriumId)
VALUES (
'Forum Cinemas Kaunas',
1,
1);

UPDATE Cinemas
SET Name = 'Forum Cinemas Vingis', City = 'Vilnius', Auditorium = 'Salė 8'
WHERE Id = 1;

SELECT * FROM Cinemas;

CREATE TABLE RatingLabelValues(
`Id` INT PRIMARY KEY AUTO_INCREMENT,
`RatingType` CHAR(5) NOT NULL
);

INSERT INTO RatingLabelValues(RatingType)
VALUES ('N13');

UPDATE RatingLabelValues SET RatingType = 'V' WHERE Id = 1;

INSERT INTO RatingLabelValues(RatingType)
VALUES
('N7'),
('N13'),
('N16'),
('N18');

SELECT * FROM RatingLabelValues;

CREATE TABLE Languages(
`Id` INT PRIMARY KEY AUTO_INCREMENT,
`LanguageType` NVARCHAR(35) NOT NULL
);


UPDATE Languages SET LanguageType = 'English' WHERE Id = 1;
INSERT INTO Languages(LanguageType)
VALUES 
('Lithuanian'),
('French'),
('Spanish'),
('Italian'),
('German'),
('Russian'),
('Swedish'),
('Polish');

INSERT INTO Languages(LanguageType)
VALUES ('Dubbed'), ('None');

SELECT * FROM Languages;

CREATE TABLE Movies(

INSERT INTO Movies(
TitleInLT,
TitleInEN,
Description,
ReleaseYear,
Genre,
RatingLabelValueId,
LanguageId,
RatingsInStars,
ShowDate,
ShowTime)
VALUES (
'Bado žaidynės: sakmė apie strazdą ir gyvatę',
'The Hunger Games: The Ballad of Songbirds and Snakes',
'Penktasis „Bado žaidynių“ serijos filmas žiūrovą nukels į priešistorę – 
likus 64-iems metams iki pirmojo serijos filmo įvykių. 
Panemo sostinėje startuoja pasiruošimas jubiliejinėms, dešimtosioms „Bado žaidynėms“. 
Šiam reginiui vadovauja žaidynių sumanytojai Kaska Haibotomas ir 
daktarė Volumnija Gaul. Tuo tarpu kažkada garbingos, o dabar nusigyvenusios Snou giminės atstovas 
Koriolanas pasiryžęs padaryti viską, kad susigrąžintų garbingą vardą ir deramą padėtį visuomenėje. 
Jis tampa vienu iš žaidynių kuratorių, kuriam pavedama prižiūrėti maištingosios 
12-os apskrities atstovę Liusę. Ji greitai atkreipia visų žiūrovų dėmesį netikėtu talentu.
Koriolanas, suvokęs, kad jo globotinė gali tapti ir jo paties bilietu į aukštesnį lygį, sutelkia 
visas jėgas, kad padėtų jai laimėti.',
2023,
'Veiksmo, drama, nuotykių',
1,
1,
4,
'2023-11-25',
'17:00');

ALTER TABLE Movies MODIFY COLUMN RatingsInStars DECIMAL(2, 1);
SELECT * FROM Movies;

CREATE TABLE CreditCardHolders(
`Id` INT PRIMARY KEY AUTO_INCREMENT,
`Name` NVARCHAR(50) NOT NULL,
`Surname` NVARCHAR(50) NOT NULL,
`CreditCardType` NVARCHAR(50) NOT NULL,
`CreditCardNumber` VARCHAR(100) NOT NULL,
`Expiry` VARCHAR(25) NOT NULL,
`BillingAdress` NVARCHAR(100) NOT NULL,
`PhoneNumber` VARCHAR(15) NOT NULL,
`Email` NVARCHAR(100) NOT NULL,
`CreatedAt` DATETIME NOT NULL
);

INSERT INTO CreditCardHolders(
Name,
Surname,
CreditCardType,
CreditCardNumber,
Expiry,
SecurityCode,
BillingAdress,
PhoneNumber,
Email)
VALUES (
'Aivaras',
'Aivaraitis',
'Debit Mastercard',
1234567890123456,
'2025-12-05',
123,
'Testu g. 24-16, Kaunas, Lietuva, LT-44423',
'+37067777777',
'aivaras@gmail.com');

SELECT * FROM CreditCardHolders;

CREATE TABLE TicketTypes(
`Id` INT PRIMARY KEY AUTO_INCREMENT,
`TicketType` VARCHAR(50) NOT NULL
);

ALTER TABLE Creditcardholders
ADD UNIQUE (Email);

INSERT INTO TicketTypes(TicketType)
VALUES ('Šeimos bilietas (1 suaugęs ir 1 vaikas iki 10 metų');

UPDATE TicketTypes
SET TicketType = 'Šeimos bilietas(1 suaugęs ir 1 vaikas iki 10 metų)'
WHERE Id = 1;

SELECT * FROM TicketTypes;

CREATE TABLE Tickets(
`Id` INT PRIMARY KEY AUTO_INCREMENT,
`Price` DECIMAL(10,2) NOT NULL,
`Currency` CHAR(3) NOT NULL,
`TicketTypeId` INT NOT NULL,
FOREIGN KEY (TicketTypeId) REFERENCES TicketTypes(Id)
);

INSERT INTO Tickets(Price, Currency, TicketTypeId)
VALUES (12.82, 'Eur', 1);

SELECT * FROM Tickets;

CREATE TABLE Orders(
`Id` INT PRIMARY KEY AUTO_INCREMENT,
`CreatedAt` DATETIME NOT NULL,
`CreditCardHolderId` INT NOT NULL,
`MovieId` INT NOT NULL,
`TicketId` INT NOT NULL,
`OrderStatus` INT NOT NULL,
FOREIGN KEY (CreditCardHolderId) REFERENCES CreditCardHolders(Id),
FOREIGN KEY (MovieId) REFERENCES Movies(Id),
FOREIGN KEY (TicketId) REFERENCES Tickets(Id)
);

INSERT INTO Orders(
CreatedAt,
CreditCardHolderId,
MovieId,
TicketId,
OrderStatus)
VALUES (CURRENT_TIMESTAMP, 1, 1, 3, 0);

SELECT * FROM Orders;


INSERT INTO `db_aa1693_charpmo`.`movies`
(`Id`,
`TitleInLT`,
`TitleInOriginalLanguage`,
`Description`,
`ReleaseYear`,
`Genre`,
`RatingLabelValueId`,
`LanguageId`,
`RatingsInStars`,
`ShowDate`,
`ShowTime`,
`CreatedAt`,
`SubtitleLanguageId`)
VALUES
(<{Id: }>,
<{TitleInLT: }>,
<{TitleInOriginalLanguage: }>,
<{Description: }>,
<{ReleaseYear: }>,
<{Genre: }>,
<{RatingLabelValueId: }>,
<{LanguageId: }>,
<{RatingsInStars: }>,
<{ShowDate: }>,
<{ShowTime: }>,
<{CreatedAt: }>,
<{SubtitleLanguageId: }>);
