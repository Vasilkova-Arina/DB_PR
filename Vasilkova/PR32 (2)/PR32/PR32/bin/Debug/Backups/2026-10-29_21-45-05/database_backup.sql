SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS `Role`;
CREATE TABLE `role` (
  `RoleID` int NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Role` (`RoleID`, `RoleName`) VALUES (1, 'Администратор');
INSERT INTO `Role` (`RoleID`, `RoleName`) VALUES (2, 'Менеджер');
INSERT INTO `Role` (`RoleID`, `RoleName`) VALUES (3, 'Клиент');

DROP TABLE IF EXISTS `User`;
CREATE TABLE `user` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `UserSurname` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `UserName` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `UserPatronymic` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `UserLogin` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `UserPassword` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `UserRole` int NOT NULL,
  PRIMARY KEY (`UserID`),
  KEY `UserRole` (`UserRole`),
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`UserRole`) REFERENCES `role` (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (1, 'Якушев ', 'Евсей', 'Лукьевич', '3', '3', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (2, 'Фёдоров ', 'Святослав', 'Григорьевич', 'loginDEnsa2018', 'LdqH+T', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (3, 'Борисов ', 'Герман', 'Дамирович', 'loginDErxm2018', '8EavEy', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (4, 'Ситников ', 'Серапион', 'Фролович', 'loginDEaic2018', 'X2adoa', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (5, 'Третьяков ', 'Валерьян', 'Иринеевич', 'loginDEwul2018', 'uK&3Zr', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (6, 'Комиссарова ', 'Мария', 'Владимировна', 'loginDEjgl2018', '++04Tb', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (7, 'Меркушева ', 'Раиса', 'Владленовна', 'loginDEgtk2018', 'pNWXhi', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (8, 'Калашникова ', 'Венера', 'Якуновна', 'loginDEvrd2018', 'S7N9hz', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (9, 'Комиссаров ', 'Семён', 'Павлович', '1', '1', 1);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (10, 'Денисов ', 'Митрофан', 'Егорович', '2', '2', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (11, 'Матвиенко ', 'Дамир', 'Богданович', 'loginDEnbu2018', '86uDLd', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (12, 'Кириллов ', 'Константин', 'Алексеевич', 'loginDEiik2018', 'gKt2zV', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (13, 'Медведьев ', 'Фёдор', 'Мэлсович', 'loginDEwyi2018', '9eskgK', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (14, 'Карпов ', 'Евгений', 'Лукьевич', 'loginDEpfr2018', 'mW1Q36', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (15, 'Маркова ', 'Евдокия', 'Артёмовна', 'loginDEjct2018', '3WpoK9', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (16, 'Красильников ', 'Тихон', 'Богданович', 'loginDEsmg2018', 'ApH1By', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (17, 'Титов ', 'Семён', 'Иринеевич', 'loginDEexu2018', 'Nt44pG', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (18, 'Кудряшов ', 'Борис', 'Иринеевич', 'loginDEztr2018', 'MYCgB7', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (19, 'Гаврилова ', 'Нинель', 'Денисовна', 'loginDEwrc2018', 'SktJa|', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (20, 'Быков ', 'Дмитрий', 'Валерьянович', 'loginDEzjs2018', '|x{s+X', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (21, 'Фомичёв ', 'Денис', 'Федосеевич', 'loginDEeka2018', 'mLZvLv', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (22, 'Белова ', 'Марфа', 'Матвеевна', 'loginDEepr2018', 'BG6tpN', 1);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (23, 'Романова ', 'Марина', 'Лаврентьевна', 'loginDEsnq2018', 'hrD}}g', 1);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (24, 'Беспалов ', 'Демьян', 'Витальевич', 'loginDEvqn2018', 'LPa|e3', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (25, 'Архипова ', 'Венера', 'Демьяновна', 'loginDEery2018', '*I0Rdi', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (26, 'Носков ', 'Парфений', 'Георгьевич', 'loginDElqv2018', 'Hqfw17', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (27, 'Зыков ', 'Иван', 'Варламович', 'loginDEtuz2018', 'Yln7JW', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (28, 'Иван ', 'Протасьевна', '', 'loginDEllr2018', 'hXtdCD', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (29, 'Рожков ', 'Протасий', 'Альвианович', 'loginDEisy2018', '5k5dHN', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (30, 'Большакова ', 'Нинель', 'Протасьевна', 'loginDEqiv2018', 'h+N2uW', 1);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (31, 'Наумова ', 'Лидия', 'Донатовна', 'loginDEmfu2018', '{ZpDBn', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (32, 'Панова ', 'Ольга', 'Олеговна', 'loginDEgbd2018', '+86Nf*', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (33, 'Комаров ', 'Аркадий', 'Иванович', 'loginDEkdg2018', 'R0tt07', 1);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (34, 'Федосеева ', 'Тамара', 'Михаиловна', 'loginDEjrs2018', 'MVg{yd', 1);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (35, 'Пестов ', 'Роман', 'Михаилович', 'loginDEmvd2018', 'wyLDa{', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (36, 'Блинов ', 'Артём', 'Ильяович', 'loginDEctc2018', 'B&dlx+', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (37, 'Владимирова ', 'Полина', 'Иринеевна', 'loginDEavf2018', 'oDTttg', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (38, 'Силин ', 'Игнатий', 'Яковович', 'loginDEako2018', 'tD8J5+', 1);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (39, 'Кононов ', 'Геннадий', 'Созонович', 'loginDEzrg2018', 'WXIgGi', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (40, 'Дьячков ', 'Фрол', 'Арсеньевич', 'loginDEdwq2018', 'WkTaBP', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (41, 'Горбачёв ', 'Арсений', 'Григорьевич', 'loginDEszg2018', 'NWkAVP', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (42, 'Виноградов ', 'Яков', 'Онисимович', 'loginDEmeh2018', 'HQ+m4W', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (43, 'Лаврентьева ', 'Валентина', 'Васильевна', 'loginDEpwm2018', 'be7AT0', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (44, 'Брагин ', 'Лукьян', 'Мартынович', 'loginDEnaq2018', 'I8c5EB', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (45, 'Трофимов ', 'Кондрат', 'Игоревич', 'loginDEtuk2018', '6aDAzV', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (46, 'Степанова ', 'Глафира', 'Авксентьевна', 'loginDEhsb2018', 'n|I6A0', 3);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (47, 'Федосеев ', 'Пётр', 'Григорьевич', 'loginDEaoi2018', 'Dl58m|', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (48, 'Поляков ', 'Николай', 'Антонович', 'loginDErmk2018', 'D3GuIv', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (49, 'Медведев ', 'Владимир', 'Онисимович', 'loginDEfnd2018', '74D9|d', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (50, 'Иван ', 'Мэлоровна', '', 'loginDEtza2018', 'A7Qldh', 2);
INSERT INTO `User` (`UserID`, `UserSurname`, `UserName`, `UserPatronymic`, `UserLogin`, `UserPassword`, `UserRole`) VALUES (51, 'Василькова', 'Арина ', 'Андреевна', 'admin', 'admin', 1);

DROP TABLE IF EXISTS `Unit`;
CREATE TABLE `unit` (
  `UnitID` int NOT NULL AUTO_INCREMENT,
  `UnitName` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`UnitID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Unit` (`UnitID`, `UnitName`) VALUES (1, 'шт.');
INSERT INTO `Unit` (`UnitID`, `UnitName`) VALUES (2, 'уп.');

DROP TABLE IF EXISTS `Sallery`;
CREATE TABLE `sallery` (
  `SalleryID` int NOT NULL AUTO_INCREMENT,
  `SalleryName` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`SalleryID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Sallery` (`SalleryID`, `SalleryName`) VALUES (1, '220-volt');
INSERT INTO `Sallery` (`SalleryID`, `SalleryName`) VALUES (2, 'Максидом');

DROP TABLE IF EXISTS `Category`;
CREATE TABLE `category` (
  `CategoryID` int NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Category` (`CategoryID`, `CategoryName`) VALUES (1, 'Зарядные устройства');
INSERT INTO `Category` (`CategoryID`, `CategoryName`) VALUES (2, 'Съемники подшипников');
INSERT INTO `Category` (`CategoryID`, `CategoryName`) VALUES (3, 'Автозапчасти');
INSERT INTO `Category` (`CategoryID`, `CategoryName`) VALUES (4, 'Ручные инструменты');
INSERT INTO `Category` (`CategoryID`, `CategoryName`) VALUES (5, 'Аксессуары');
INSERT INTO `Category` (`CategoryID`, `CategoryName`) VALUES (6, 'Автосервис');

DROP TABLE IF EXISTS `Product`;
CREATE TABLE `product` (
  `ProductArticleNumber` varchar(6) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProductName` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProductUnit` int NOT NULL,
  `ProductCost` decimal(19,4) NOT NULL,
  `ProductDiscountAmountMax` tinyint DEFAULT NULL,
  `ProductManufacturer` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProductSallery` int NOT NULL,
  `ProductCategory` int NOT NULL,
  `ProductDiscountAmount` tinyint DEFAULT NULL,
  `ProductQuantityInStock` int NOT NULL,
  `ProductDescription` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProductPhoto` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`ProductArticleNumber`),
  KEY `ProductUnit` (`ProductUnit`),
  KEY `ProductSallery` (`ProductSallery`),
  KEY `ProductCategory` (`ProductCategory`),
  CONSTRAINT `product_ibfk_1` FOREIGN KEY (`ProductUnit`) REFERENCES `unit` (`UnitID`),
  CONSTRAINT `product_ibfk_2` FOREIGN KEY (`ProductSallery`) REFERENCES `sallery` (`SalleryID`),
  CONSTRAINT `product_ibfk_3` FOREIGN KEY (`ProductCategory`) REFERENCES `category` (`CategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('A782R4', 'Аккумулятор', 2, 4500,0000, '30', 'BIG FIGHTER', 1, 3, '20', 24, 'Аккумулятор автомобильный BIG FIGHTER 55р', 'A782R4.jpg');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('B702T6', 'Домкрат', 1, 2700,0000, '10', 'ALCA', 1, 6, '2', 3, 'Домкрат ALCA 436000', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('D374E4', 'Съемник подшипников', 1, 1400,0000, '20', 'AIRLINE', 1, 2, '3', 2, 'Съемник AIRLINE AT-GP2-05', 'D374E4.jpeg');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('D799T6', 'Съемник подшипников', 1, 1800,0000, '25', 'JTC', 1, 2, '2', 6, 'Съемник для подшипников JTC 9000', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('E679R3', 'Автошампунь', 1, 4000,0000, '15', 'GRASS', 1, 6, '4', 14, 'Автошампунь GRASS 800026 Active Foam Truck', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('E932T8', 'Полироль', 1, 2100,0000, '25', 'GRASS', 1, 6, '3', 23, 'Полироль GRASS 125101 Black Brilliance', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('F026R4', 'Антифриз', 1, 530,0000, '15', 'MOBIL', 1, 6, '2', 13, 'Антифриз сине-зеленый MOBIL ANTIFREEZE EXTRA', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('F938T5', 'Антифриз', 1, 1200,0000, '15', 'TCL', 1, 6, '4', 34, 'Антифриз красный TCL LLC01212', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('H572T6', 'Парктроник', 1, 2900,0000, '15', 'AIRLINE', 1, 3, '5', 12, 'Парктроник AIRLINE APS-8L-02', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('K702L6', 'Ключ', 1, 1600,0000, '15', 'JONNESWAY', 1, 4, '3', 9, 'Ключ JONNESWAY W233032 (30 / 32 мм)', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('K830R4', 'Колпак для колеса', 2, 915,0000, '20', 'AIRLINE', 2, 3, '3', 14, 'Колпак для колеса AIRLINE Супер Астра R16 серебристый 2шт', 'K830R4.jpg');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('K849L6', 'Набор ключей', 2, 780,0000, '15', 'STV', 1, 4, '2', 23, 'Набор ключей накидных STV 00-00010990 6шт.', 'K849L6.jpeg');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('L802Y5', 'Лопата', 1, 870,0000, '5', 'AIRLINE', 1, 5, '4', 23, 'Лопата саперная AIRLINE AB-S-03', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('M562Y7', 'Мультиметр', 1, 14200,0000, '5', 'JTC', 2, 5, '3', 12, 'Мультиметр JTC 1227A автомобильный', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('O393R4', 'Отвертка', 1, 460,0000, '15', 'JONNESWAY', 1, 4, '3', 14, 'Отвертка JONNESWAY D04P2100', 'O393R4.jpeg');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('P023T2', 'Провода прикуривания', 1, 3400,0000, '20', 'SMART', 2, 3, '2', 6, 'Провода прикуривания в сумке SMART CABLE 700 4,5м', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('P307T5', 'Провода прикуривания', 1, 700,0000, '10', 'EXPERT', 2, 3, '4', 2, 'Провода прикуривания в сумке EXPERT 400А 2,5м', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('P439R4', 'Пассатижи', 1, 310,0000, '5', 'HAMMER', 2, 5, '3', 34, 'Пассатижи HAMMER Flex 601-050 160 мм (6 дюймов)', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('S021R4', 'Адаптер для щеток', 2, 200,0000, '3', 'ALCA', 2, 3, '2', 13, 'Адаптер для щеток стеклоочистителя ALCA Top Lock A/C блистер 2 шт', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('S037R9', 'Щетка', 1, 740,0000, '25', 'AIRLINE', 1, 5, '2', 26, 'Щётка AIRLINE AB-H-03', 'S037R9.jpeg');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('S625T5', 'Щетка', 1, 249,0000, '20', 'ALCA', 2, 3, '3', 12, 'щетка стеклоочистителя ALCA Start 16"/40см каркасная"', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('S826R4', 'Щетка', 1, 530,0000, '2', 'ALCA', 2, 3, '4', 28, 'Щетка стеклоочистителя ALCA Super flat 19"/48см бескаркасная"', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('S983R4', 'Щетка', 1, 500,0000, '15', 'BOSCH', 2, 3, '4', 8, 'Щетка с/о BOSCH ECO 65C 650мм каркасная', 'S983R4.jpg');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('T589T6', 'Термометр', 1, 1400,0000, '10', 'ALCA', 1, 5, '2', 3, 'Термометр ALCA 577000', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('V892T6', 'Свеча зажигания', 1, 130,0000, '5', 'CHAMPION', 2, 3, '3', 21, 'Свеча зажигания CHAMPION IGP F7RTC', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('Z326T9', 'Зарядное устройство', 1, 2400,0000, '15', 'EXPERT', 2, 1, '3', 14, 'Устройство зарядное EXPERT ЗУ-300 6/12В 3,8А', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('Z374R3', 'Зарядное устройство', 1, 4600,0000, '25', 'AIRLINE', 1, 1, '2', 14, 'Зарядное устройство AIRLINE ACH-15A-08', 'Z374R3.jpeg');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('Z469T7', 'Устройство пуско-зарядное', 1, 4000,0000, '25', 'AIRLINE', 2, 1, '2', 4, 'Устройство пуско-зарядное AIRLINE 12В 8000мАч 350А', 'Z469T7.jpg');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('Z472R4', 'Зарядное устройство', 1, 1250,0000, '30', 'KOLNER', 1, 1, '4', 6, 'Зарядное устройство KOLNER KBCН 4', 'Z472R4.jpeg');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('Z782T5', 'Зажим', 2, 290,0000, '25', 'AIRLINE', 1, 1, '5', 6, 'Зажим AIRLINE SA-400-P', '');
INSERT INTO `Product` (`ProductArticleNumber`, `ProductName`, `ProductUnit`, `ProductCost`, `ProductDiscountAmountMax`, `ProductManufacturer`, `ProductSallery`, `ProductCategory`, `ProductDiscountAmount`, `ProductQuantityInStock`, `ProductDescription`, `ProductPhoto`) VALUES ('А00001', 'ву', 1, 10,0000, '10', 'фвы', 1, 3, '1', 10, 'выс', 'glysh.jpg');

DROP TABLE IF EXISTS `Status`;
CREATE TABLE `status` (
  `StatusID` int NOT NULL AUTO_INCREMENT,
  `StatusName` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`StatusID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Status` (`StatusID`, `StatusName`) VALUES (1, 'Новый ');
INSERT INTO `Status` (`StatusID`, `StatusName`) VALUES (2, 'Завершен');

DROP TABLE IF EXISTS `PickupPoint`;
CREATE TABLE `pickuppoint` (
  `PickupPointID` int NOT NULL AUTO_INCREMENT,
  `PickupPointCity` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `PickupPointStreet` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `PickupPointHome` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`PickupPointID`)
) ENGINE=InnoDB AUTO_INCREMENT=660541 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (125061, ' г. Вологда', ' ул. Подгорная', '8');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (125703, ' г. Вологда', ' ул. Партизанская', '49');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (125837, ' г. Вологда', ' ул. Шоссейная', '40');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (190949, ' г. Вологда', ' ул. Мичурина', '26');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (344288, ' г. Вологда', ' ул. Чехова', '1');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (394060, ' г.Вологда', ' ул. Фрунзе', '43');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (394242, ' г. Вологда', ' ул. Коммунистическая', '43');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (394782, ' г. Вологда', ' ул. Чехова', '3');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (400562, ' г. Вологда', ' ул. Зеленая', '32');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (410172, ' г. Вологда', ' ул. Северная', '13');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (410542, ' г. Вологда', ' ул. Светлая', '46');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (410661, ' г. Вологда', ' ул. Школьная', '50');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (420151, ' г. Вологда', ' ул. Вишневая', '32');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (426030, ' г. Вологда', ' ул. Маяковского', '44');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (443890, ' г. Вологда', ' ул. Коммунистическая', '1');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (450375, ' г. Вологда ', 'ул. Клубная', '44');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (450558, ' г. Вологда', ' ул. Набережная', '30');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (450983, ' г.Вологда', ' ул. Комсомольская', '26');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (454311, ' г.Вологда', ' ул. Новая', '19');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (603002, ' г. Вологда', ' ул. Дзержинского', '28');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (603036, ' г. Вологда', ' ул. Садовая', '4');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (603379, ' г. Вологда', ' ул. Спортивная', '46');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (603721, ' г. Вологда', ' ул. Гоголя', '41');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (614164, ' г.Вологда', '  ул. Степная', '30');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (614510, ' г. Вологда', ' ул. Маяковского', '47');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (614611, ' г. Вологда', ' ул. Молодежная', '50');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (614753, ' г. Вологда', ' ул. Полевая', '35');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (620839, ' г. Вологда', ' ул. Цветочная', '8');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (625283, ' г. Вологда', ' ул. Победы', '46');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (625560, ' г. Вологда', ' ул. Некрасова', '12');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (625590, ' г. Вологда', ' ул. Коммунистическая', '20');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (625683, ' г. Вологда', ' ул. 8 Марта', '12');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (630201, ' г. Вологда', ' ул. Комсомольская', '17');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (630370, ' г. Вологда', ' ул. Шоссейная', '24');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (660007, ' г.Вологда', ' ул. Октябрьская', '19');
INSERT INTO `PickupPoint` (`PickupPointID`, `PickupPointCity`, `PickupPointStreet`, `PickupPointHome`) VALUES (660540, ' г. Вологда', ' ул. Солнечная', '25');

DROP TABLE IF EXISTS `Order`;
CREATE TABLE `order` (
  `OrderID` int NOT NULL AUTO_INCREMENT,
  `OrderDate` datetime NOT NULL,
  `OrderDeliveryDate` datetime NOT NULL,
  `OrderPickupPoint` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `OrderClient` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `OrderCode` int NOT NULL,
  `OrderStatus` int NOT NULL,
  PRIMARY KEY (`OrderID`),
  KEY `OrderStatus` (`OrderStatus`),
  CONSTRAINT `order_ibfk_1` FOREIGN KEY (`OrderStatus`) REFERENCES `status` (`StatusID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (1, '03.05.2022 0:00:00', '09.05.2022 0:00:00', '1', 'Никитин Артур Алексеевич', 211, 1);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (2, '04.05.2022 0:00:00', '10.05.2022 0:00:00', '3', 'Никитин Артур Алексеевич', 212, 2);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (3, '05.05.2022 0:00:00', '11.05.2022 0:00:00', '5', 'Климов Арсений Тимурович', 213, 1);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (4, '06.05.2022 0:00:00', '12.05.2022 0:00:00', '6', 'Климов Арсений Тимурович', 214, 2);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (5, '07.05.2022 0:00:00', '13.05.2022 0:00:00', '7', 'Киселев Максим Сергеевич', 215, 2);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (6, '08.05.2022 0:00:00', '14.05.2022 0:00:00', '10', 'Борисов Тимур Егорович', 216, 1);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (7, '09.05.2022 0:00:00', '15.05.2022 0:00:00', '11', 'Климов Арсений Тимурович', 217, 1);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (8, '10.05.2022 0:00:00', '16.05.2022 0:00:00', '20', 'Климов Арсений Тимурович', 218, 1);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (9, '11.05.2022 0:00:00', '17.05.2022 0:00:00', '30', 'Никитин Артур Алексеевич', 219, 2);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (10, '12.05.2022 0:00:00', '18.05.2022 0:00:00', '33', 'Борисов Тимур Егорович', 220, 1);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (11, '14.10.2026 19:25:44', '20.10.2026 19:25:44', ' г. Вологда,  ул. Подгорная, 8', 'ФЫВАПРОЛДЖ', 747, 1);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (12, '14.10.2026 19:27:03', '20.10.2026 19:27:03', ' г. Вологда,  ул. Подгорная, 8', 'в', 772, 1);
INSERT INTO `Order` (`OrderID`, `OrderDate`, `OrderDeliveryDate`, `OrderPickupPoint`, `OrderClient`, `OrderCode`, `OrderStatus`) VALUES (13, '14.10.2026 19:27:29', '20.10.2026 19:27:29', ' г. Вологда,  ул. Подгорная, 8', 'в', 512, 1);

DROP TABLE IF EXISTS `OrderProduct`;
CREATE TABLE `orderproduct` (
  `OrderID` int NOT NULL,
  `ProductArticleNumber` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProductCount` int NOT NULL,
  PRIMARY KEY (`OrderID`,`ProductArticleNumber`),
  KEY `ProductArticleNumber` (`ProductArticleNumber`),
  CONSTRAINT `orderproduct_ibfk_1` FOREIGN KEY (`OrderID`) REFERENCES `order` (`OrderID`),
  CONSTRAINT `orderproduct_ibfk_2` FOREIGN KEY (`ProductArticleNumber`) REFERENCES `product` (`ProductArticleNumber`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (1, 'D374E4', 2);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (1, 'Z472R4', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (2, 'A782R4', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (2, 'K830R4', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (3, 'K849L6', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (3, 'O393R4', 2);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (4, 'S983R4', 3);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (4, 'Z469T7', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (5, 'F938T5', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (5, 'S037R9', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (6, 'D799T6', 2);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (6, 'E679R3', 2);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (7, 'P023T2', 2);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (7, 'V892T6', 2);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (8, 'K702L6', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (8, 'S625T5', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (9, 'L802Y5', 3);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (9, 'P307T5', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (10, 'B702T6', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (10, 'M562Y7', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (11, 'B702T6', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (12, 'B702T6', 1);
INSERT INTO `OrderProduct` (`OrderID`, `ProductArticleNumber`, `ProductCount`) VALUES (13, 'B702T6', 1);

SET FOREIGN_KEY_CHECKS = 1;
