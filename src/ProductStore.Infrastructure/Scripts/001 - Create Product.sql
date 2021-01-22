CREATE TABLE Product (
	Id uuid PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
	Description VARCHAR(100) NOT NULL,
	Price numeric(10, 2) NOT NULL,
	DeliveryPrice numeric(10, 2) NULL
) 