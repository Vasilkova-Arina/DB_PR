use db29;
create table Role
(
  RoleID int primary key auto_increment,
  RoleName varchar(100) not null
);
create table User
(
  UserID int primary key auto_increment,
  UserSurname varchar(100) not null,
  UserName varchar(100) not null,
  UserPatronymic varchar(100),
  UserLogin text not null,
  UserPassword text not null,
  UserRole int not null,
  foreign key (UserRole) references Role(RoleID) 
);
create table Unit
(
  UnitID int primary key auto_increment,
  UnitName varchar(100) not null
);
create table Sallery
(
  SalleryID int primary key auto_increment,
  SalleryName varchar(100) not null
);
create table Category
(
  CategoryID int primary key auto_increment,
  CategoryName varchar(100) not null
);
create table Product
(
  ProductArticleNumber varchar(6) primary key,
  ProductName varchar(100) not null,
  ProductUnit int not null,
  ProductCost decimal(19,4) not null,
  ProductDiscountAmountMax tinyint null,
  ProductManufacturer varchar(100) not null,
  ProductSallery int not null,
  ProductCategory int not null,
  ProductDiscountAmount tinyint null,
  ProductQuantityInStock int not null,
  ProductDescription text not null,
  ProductPhoto varchar(30) not null,
  foreign key (ProductUnit) references Unit(UnitID),
  foreign key (ProductSallery) references Sallery(SalleryID),
  foreign key (ProductCategory) references Category(CategoryID)
);
create table Status
(
  StatusID int primary key auto_increment,
  StatusName varchar(100) not null
);
create table `Order`
(
  OrderID int primary key auto_increment,
  OrderDate datetime not null,
  OrderDeliveryDate datetime not null,
  OrderPickupPoint text not null,
  OrderClient varchar(255) not null,
  OrderCode int not null,
  OrderStatus int not null,
  foreign key (OrderStatus) references Status(StatusID)
);
create table PickupPoint
(
  PickupPointID int primary key auto_increment,
  PickupPointCity varchar(100) not null,
  PickupPointStreet varchar(100) not null,
  PickupPointHome varchar(100) not null
);
create table OrderProduct
(
  OrderID int not null,
  ProductArticleNumber varchar(100)  not null,
  ProductCount int not null,
  Primary key (OrderID,ProductArticleNumber),
  foreign key (OrderID) references `Order`(OrderID),
  foreign key (ProductArticleNumber) references Product(ProductArticleNumber)
);