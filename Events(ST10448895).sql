use master
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'ST10448895Ease')
DROP DATABASE ST10448895Ease
CREATE DATABASE ST10448895Ease

USE ST10448895Ease

-- Table Creation
CREATE TABLE Venue(
    Venue_ID INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    [Venue_Name] VARCHAR(250) NOT NULL,
    Locations VARCHAR(250) NOT NULL,
    Capacity INT NOT NULL,  -- Changed to INT
    ImageUrl VARBINARY(MAX) NOT NULL
);

CREATE TABLE EventS(
    Event_ID INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    [Event_Name] VARCHAR(250) NOT NULL,
    Event_Date DATE NOT NULL,  -- Changed to DATE
    Descriptions VARCHAR(250) NOT NULL,
    Venue_ID INT 
);

ALTER TABLE EventS
ADD FOREIGN KEY (Venue_ID) REFERENCES Venue(Venue_ID) ON DELETE CASCADE;

CREATE TABLE Bookings(
    Booking_ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Venue_ID INT,
    Event_ID INT,
    Booking_Date DATE
);

ALTER TABLE Bookings
ADD FOREIGN KEY (Venue_ID) REFERENCES Venue(Venue_ID) ON DELETE CASCADE;

ALTER TABLE Bookings
ADD FOREIGN KEY (Event_ID) REFERENCES EventS(Event_ID) ON DELETE NO ACTION;

USE EventEase;

-- Inserting data into Venue table
INSERT INTO Venue (Venue_Name, Locations, Capacity, ImageUrl)
VALUES 
('Grand St', '123 Main', 5000, 0x);

-- Inserting data into EventS table
INSERT INTO EventS (Event_Name, Event_Date, Descriptions, Venue_ID)
VALUES 
('Concert', '2025-04-12', 'Live Show', 1);

-- Inserting data into Bookings table
INSERT INTO Bookings (Venue_ID, Event_ID, Booking_Date)
VALUES 
(1, 1, '2025-03-12');

UPDATE Venue
SET ImageUrl = CONVERT(VARBINARY(MAX), 'https://www.google.com/url?sa=i&url=https%3A%2F%2Fjunebugweddings.com%2Fwedding-blog%2Fthe-ultimate-guide-to-finding-your-wedding-venue%2F&psig=AOvVaw1xzDzLxQ8pLromXcML0MBU&ust=1741947487771000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCJCWwKDqhowDFQAAAAAdAAAAABAE')
WHERE Venue_ID = 1; 

SELECT * FROM Venue;
SELECT * FROM EventS;
SELECT * FROM Bookings;
