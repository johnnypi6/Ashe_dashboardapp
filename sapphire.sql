/*
SQLyog Ultimate v12.4.3 (64 bit)
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

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `id` int(16) NOT NULL AUTO_INCREMENT,
  `name` varchar(32) NOT NULL,
  `password` varchar(32) NOT NULL,
  `email` varchar(32) NOT NULL,
  `phone` varchar(32) DEFAULT NULL,
  `mobile` varchar(32) DEFAULT NULL,
  `address` varchar(128) DEFAULT NULL,
  `company` varchar(128) DEFAULT NULL,
  `contactperson` varchar(256) DEFAULT NULL,
  `role` enum('admin','superadmin','user') DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Data for the table `user` */

insert  into `user`(`id`,`name`,`password`,`email`,`phone`,`mobile`,`address`,`company`,`contactperson`,`role`) values 
(1,'YeJing Song1','MQAxADEAMQAxADEA','admin1@motocle.com','111222333444','111111','tokyo shibuya 123','A2ZCreatorz','1','user'),
(2,'YeJing Song2','MQAxADEAMQAxADEA','valorvalor312@hotmail.com','11122233344','111111','tokyo shibuya 123','A2ZCreatorz','12','user'),
(3,'YeJing Song12','MQAxADEAMQAxADEA','admin1@motocle.com','12312341234','111111','tokyo shibuya 123','asd','1','user');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
