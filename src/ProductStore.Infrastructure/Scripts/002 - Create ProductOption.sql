CREATE TABLE ProductOption(
	Id uuid PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
	Description VARCHAR(100) NULL,
	ProductId uuid REFERENCES Product (Id)
 )  