/*
SQLyog Community v12.09 (64 bit)
MySQL - 10.1.37-MariaDB : Database - sapphire
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`sapphire` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `sapphire`;

/*Table structure for table `__efmigrationshistory` */

DROP TABLE IF EXISTS `__efmigrationshistory`;

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `__efmigrationshistory` */

insert  into `__efmigrationshistory`(`MigrationId`,`ProductVersion`) values ('00000000000000_CreateIdentitySchema','2.1.8-servicing-32085');

/*Table structure for table `aspnetroleclaims` */

DROP TABLE IF EXISTS `aspnetroleclaims`;

CREATE TABLE `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` int(11) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `role` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetroleclaims` */

/*Table structure for table `aspnetuserclaims` */

DROP TABLE IF EXISTS `aspnetuserclaims`;

CREATE TABLE `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetuserclaims` */

/*Table structure for table `aspnetuserlogins` */

DROP TABLE IF EXISTS `aspnetuserlogins`;

CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `ProviderDisplayName` longtext,
  `UserId` int(11) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetuserlogins` */

/*Table structure for table `aspnetuserroles` */

DROP TABLE IF EXISTS `aspnetuserroles`;

CREATE TABLE `aspnetuserroles` (
  `UserId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `role` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetuserroles` */

insert  into `aspnetuserroles`(`UserId`,`RoleId`) values (1,2),(1,100),(3,1),(4,1),(6,2);

/*Table structure for table `aspnetusertokens` */

DROP TABLE IF EXISTS `aspnetusertokens`;

CREATE TABLE `aspnetusertokens` (
  `UserId` int(11) NOT NULL,
  `LoginProvider` varchar(128) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Value` longtext,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetusertokens` */

/*Table structure for table `device` */

DROP TABLE IF EXISTS `device`;

CREATE TABLE `device` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `LocationId` int(11) NOT NULL,
  `IMEI` varchar(256) NOT NULL,
  `SIMCard` varchar(256) DEFAULT NULL,
  `DeviceTypeId` int(11) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Device_UserId` (`UserId`),
  KEY `IX_Device_LocationId` (`LocationId`),
  CONSTRAINT `FK_Device_Location_LocationId` FOREIGN KEY (`LocationId`) REFERENCES `location` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Device_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

/*Data for the table `device` */

insert  into `device`(`Id`,`UserId`,`LocationId`,`IMEI`,`SIMCard`,`DeviceTypeId`,`Status`) values (1,3,1,'373782822','383892929',1,1);

/*Table structure for table `devicetype` */

DROP TABLE IF EXISTS `devicetype`;

CREATE TABLE `devicetype` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Data for the table `devicetype` */

insert  into `devicetype`(`Id`,`Name`) values (1,'Tracking'),(2,'M2M'),(3,'G.H');

/*Table structure for table `location` */

DROP TABLE IF EXISTS `location`;

CREATE TABLE `location` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) NOT NULL,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Location_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

/*Data for the table `location` */

insert  into `location`(`Id`,`Name`,`Latitude`,`Longitude`) values (1,'Beijing',0,0);

/*Table structure for table `role` */

DROP TABLE IF EXISTS `role`;

CREATE TABLE `role` (
  `Id` int(11) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `DisplayName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `role` */

insert  into `role`(`Id`,`Name`,`NormalizedName`,`DisplayName`,`ConcurrencyStamp`) values (1,'Customer','CUSTOMER','Customer','7e77e155-eca9-4a67-aba0-1e2b1533750f'),(2,'Admin','ADMIN','Administrator','1dcadabb-4174-4b32-9398-cb2348293874'),(100,'SuperAdmin','SUPERADMIN','Super Administrator','5773deaf-7b26-49d0-84e9-70565bb3fa16');

/*Table structure for table `sensor` */

DROP TABLE IF EXISTS `sensor`;

CREATE TABLE `sensor` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DeviceId` int(11) NOT NULL,
  `SensorTypeId` int(11) NOT NULL,
  `SerialNumber` varchar(256) DEFAULT NULL,
  `HighThreshold` varchar(256) DEFAULT NULL,
  `LowThreshold` varchar(256) DEFAULT NULL,
  `RelayOperation` int(11) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Sensor_DeviceId` (`DeviceId`),
  KEY `IX_Sensor_Type` (`SensorTypeId`),
  CONSTRAINT `FK_Sensor_Device_Id` FOREIGN KEY (`DeviceId`) REFERENCES `device` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*Data for the table `sensor` */

insert  into `sensor`(`Id`,`DeviceId`,`SensorTypeId`,`SerialNumber`,`HighThreshold`,`LowThreshold`,`RelayOperation`,`Status`) values (1,1,1,'324324324','10.5','1.5',0,0),(2,1,2,'345987656','6.0','0.5',0,0),(3,1,3,'569876545','7.8','2.5',0,0),(4,1,4,'906864534','6.3','5.2',0,0),(5,1,5,'863543234','7.0','2.5',0,0),(6,1,6,'214667866','3.5','1.0',0,0);

/*Table structure for table `sensortype` */

DROP TABLE IF EXISTS `sensortype`;

CREATE TABLE `sensortype` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*Data for the table `sensortype` */

insert  into `sensortype`(`Id`,`Name`) values (1,'Pressure'),(2,'Temperature'),(3,'Humidity'),(4,'Moisture'),(5,'Liquid Flow'),(6,'CO2');

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(256) NOT NULL,
  `NormalizedUserName` varchar(256) NOT NULL,
  `PasswordHash` varchar(256) NOT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `Phone` varchar(256) DEFAULT NULL,
  `Mobile` varchar(256) DEFAULT NULL,
  `Address` varchar(256) DEFAULT NULL,
  `Company` varchar(256) DEFAULT NULL,
  `ContactPerson` varchar(256) DEFAULT NULL,
  `SecurityStamp` longtext,
  `ConcurrencyStamp` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  UNIQUE KEY `UserEmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*Data for the table `user` */

insert  into `user`(`Id`,`UserName`,`NormalizedUserName`,`PasswordHash`,`Email`,`NormalizedEmail`,`Phone`,`Mobile`,`Address`,`Company`,`ContactPerson`,`SecurityStamp`,`ConcurrencyStamp`) values (1,'SuperAdmin','SUPERADMIN','AQAAAAEAACcQAAAAECnNOl8y80qXrCSSgt+ygyOwK6zsY/BadHgIa2OXs1RdIcG9ruP/Sn4N+Rfg9KysQw==','super@gamil.com','SUPER@GAMIL.COM','1+38382930','324-2343-2342','CA','Google','Ashe','6ELZSYAUOVYTI62NTJA7SNQ42MZ4HOJR','ff7a9e93-ada6-440d-9fc8-edf38f52d6ff'),(3,'Jane','JANE','AQAAAAEAACcQAAAAENhT8qxPjz2MycjBQ/tjFYPZ2wOXt3vMV6CUETQXYIeE+KnKPX6AFcrya5le+3FYMA==','jane@gg.com','JANE@GG.COM','+188 23423342','234-2343-3423','GM','GG','Jim','TSPB7KH4PJFPXBOIQS4ECY7BMH6D4ZS5','3e982c46-7466-4473-9f94-5d48245f9bb0'),(4,'John','JOHN','AQAAAAEAACcQAAAAEMB0OeVJ+VlpLcyeK4AahxeIsIHKgu9oSib3Vvuu6r/Bzo4BFzl9A50ZcvqdQbuxgw==','john@ppp.com','JOHN@PPP.COM','+44 23423423','234-5654-2423','HM','PPP','Jenny','JUFQGG4G72JZPBM335JLP5JGT4UMIS2H','da5e2d25-8044-4219-a573-f2aff7f2a4ec'),(6,'Venus','VENUS','AQAAAAEAACcQAAAAEFT2NTs6/mTuvXCwce1awtnvSyhbhR/IjiJLg/08mqULJCxgVJmdhCevcKV/6lumNg==','venus@uit.com','VENUS@UIT.COM','+44 233243434','234-2344-3242','DanDong','UIT','johnny','U5S2DUVAU6XCJ33B6LJQ4UOO2O7MQ3PP','f8406dc5-e3de-4004-9078-516096c9058a');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
