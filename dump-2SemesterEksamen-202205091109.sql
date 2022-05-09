-- MySQL dump 10.13  Distrib 5.5.62, for Win64 (AMD64)
--
-- Host: bound1937.asuscomm.com    Database: 2SemesterEksamen
-- ------------------------------------------------------
-- Server version	5.5.5-10.5.15-MariaDB-0+deb11u1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `account`
--

DROP TABLE IF EXISTS `account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account` (
  `username` varchar(40) NOT NULL,
  `password` blob NOT NULL,
  `privilege` varchar(20) NOT NULL,
  `phone_number` varchar(20) NOT NULL,
  `first_names` varchar(100) NOT NULL,
  `last_name` varchar(100) NOT NULL,
  `type` varchar(30) NOT NULL,
  `creation_date` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`username`),
  KEY `account_FK` (`type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account`
--

LOCK TABLES `account` WRITE;
/*!40000 ALTER TABLE `account` DISABLE KEYS */;
INSERT INTO `account` VALUES ('A','¸¥|ì?áÅ§‘˙È/˝','admin','11111111','AA','BB','internal','2022-05-09 11:00:11'),('Bo','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Bo','Bosen','youth','2022-05-09 11:00:11'),('BoundSoul','¸¥|ì?áÅ§‘˙È/˝','resident','11111111','Simon','Klausen','youth','2022-05-09 11:00:11'),('dda','U)˙iæ:µΩâR\0LOŒZ','resident','11111111','Das','Dausen','normal','2022-05-09 11:00:11'),('D√∏medk√∏lle','¸¥|ì?áÅ§‘˙È/˝','waitlist','Dolf','Klausen','88888888','normal','2022-05-09 11:00:11'),('hhh','¸¥|ì?áÅ§‘˙È/˝','resident','11111111','DAAA','ddd','normal','2022-05-09 11:00:11'),('Jan','¸¥|ì?áÅ§‘˙È/˝','resident','11111111','Bo','Janden','senior','2022-05-09 11:00:11'),('jkj','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Jesper','Flimsen','youth','2022-05-09 11:00:11'),('Knudsen48','¸¥|ì?áÅ§‘˙È/˝','resident','11111111','Knud','Knudsen','normal','2022-05-09 11:00:11'),('Lars','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Lars','Snasken','youth','2022-05-09 11:00:11'),('normie420','Tπ)S3ê®gTmÀ@Dıe5','resident','11111111','Tank','Dasken','senior','2022-05-09 11:00:11'),('oleee400','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Ole','Monsen','normal','2022-05-09 11:00:11'),('Ovemove','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Ove','Musken','senior','2022-05-09 11:00:11'),('S','¸¥|ì?áÅ§‘˙È/˝','secretary','11111111','Sam','Ausen','internal','2022-05-09 11:00:11'),('TimIsCool','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Tim','Dusen','normal','2022-05-09 11:00:11');
/*!40000 ALTER TABLE `account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `account_resource_reservations`
--

DROP TABLE IF EXISTS `account_resource_reservations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account_resource_reservations` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `account_username` varchar(40) NOT NULL,
  `resource_id` int(11) NOT NULL,
  `start_timestamp` datetime NOT NULL DEFAULT current_timestamp(),
  `end_timestamp` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  KEY `resident_resource_reservations_FK_1` (`resource_id`),
  KEY `account_resource_reservations_FK` (`account_username`),
  CONSTRAINT `account_resource_reservations_FK` FOREIGN KEY (`account_username`) REFERENCES `account` (`username`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `resident_resource_reservations_FK_1` FOREIGN KEY (`resource_id`) REFERENCES `resource` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=73 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_resource_reservations`
--

LOCK TABLES `account_resource_reservations` WRITE;
/*!40000 ALTER TABLE `account_resource_reservations` DISABLE KEYS */;
INSERT INTO `account_resource_reservations` VALUES (1,'BoundSoul',4,'2022-04-29 00:40:48','2022-04-29 00:40:48'),(25,'BoundSoul',15,'2022-04-29 03:03:58','2022-04-29 06:03:58'),(27,'BoundSoul',1,'2022-04-30 17:20:28','2022-04-30 19:20:28'),(29,'BoundSoul',2,'2022-05-01 17:24:17','2022-05-01 18:24:17'),(56,'BoundSoul',14,'2022-05-04 13:12:00','2022-05-04 22:12:00'),(57,'BoundSoul',1,'2022-05-04 13:00:00','2022-05-04 17:00:00'),(58,'BoundSoul',2,'2022-05-04 13:00:00','2022-05-04 17:00:00'),(64,'BoundSoul',7,'2022-05-05 10:45:00','2022-05-05 11:45:00'),(69,'Knudsen48',3,'2022-05-05 10:45:00','2022-05-05 11:45:00'),(70,'hhh',10,'2022-05-05 10:45:00','2022-05-05 11:45:00'),(71,'BoundSoul',3,'2022-05-05 16:00:00','2022-05-05 20:00:00'),(72,'BoundSoul',9,'2022-05-09 11:23:00','2022-05-09 13:23:00');
/*!40000 ALTER TABLE `account_resource_reservations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `housing`
--

DROP TABLE IF EXISTS `housing`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `housing` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` varchar(30) NOT NULL,
  `m2` int(11) NOT NULL,
  `rental_price` double NOT NULL,
  `street_address` varchar(100) NOT NULL,
  `locality_postal_code` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `housing_FK` (`type`),
  KEY `housing_FK_1` (`locality_postal_code`),
  CONSTRAINT `housing_FK_1` FOREIGN KEY (`locality_postal_code`) REFERENCES `locality` (`postal_code`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `housing`
--

LOCK TABLES `housing` WRITE;
/*!40000 ALTER TABLE `housing` DISABLE KEYS */;
INSERT INTO `housing` VALUES (1,'normal',53,8700,'Fjordvejen 49',7000),(2,'youth',46,4600,'Fjordvejen 50',7000),(3,'senior',80,9000,'Fjordvejen 51',7000),(4,'normal',135,11000,'Fjordvejen 52',7000),(5,'normal',75,6500,'Fjordvejen 53',7000),(6,'youth',44,4200,'Langelinie 29',7100),(8,'senior',88,7500,'Langelinie 30',7100),(9,'normal',78,10000,'Langelinie 31',7100),(10,'normal',100,9500,'Worsaaesgade 9',7100),(12,'youth',77,999,'Worsaaesgade 10',7100),(13,'normal',70,6000,'Strandgade 3',7100),(14,'youth',22,4400,'Boulevarden 1',7100);
/*!40000 ALTER TABLE `housing` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `housing_account`
--

DROP TABLE IF EXISTS `housing_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `housing_account` (
  `housing_id` int(11) NOT NULL,
  `account_username` varchar(40) NOT NULL,
  `start_contract` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`housing_id`),
  KEY `housing_residents_FK` (`account_username`),
  CONSTRAINT `housing_residents_FK` FOREIGN KEY (`account_username`) REFERENCES `account` (`username`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `housing_residents_FK_1` FOREIGN KEY (`housing_id`) REFERENCES `housing` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `housing_account`
--

LOCK TABLES `housing_account` WRITE;
/*!40000 ALTER TABLE `housing_account` DISABLE KEYS */;
INSERT INTO `housing_account` VALUES (1,'Knudsen48','2022-05-04 10:50:12'),(2,'BoundSoul','2022-04-27 10:53:34'),(3,'Jan','2022-05-05 10:36:12'),(4,'hhh','2022-05-04 20:13:56'),(5,'dda','2022-05-04 09:56:03'),(8,'normie420','2022-05-04 10:50:59');
/*!40000 ALTER TABLE `housing_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `locality`
--

DROP TABLE IF EXISTS `locality`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `locality` (
  `postal_code` int(11) NOT NULL,
  `city` varchar(30) NOT NULL,
  PRIMARY KEY (`postal_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `locality`
--

LOCK TABLES `locality` WRITE;
/*!40000 ALTER TABLE `locality` DISABLE KEYS */;
INSERT INTO `locality` VALUES (7000,'Fredericia'),(7100,'Vejle');
/*!40000 ALTER TABLE `locality` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `resource`
--

DROP TABLE IF EXISTS `resource`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `resource` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` varchar(30) NOT NULL,
  `times_reserved` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `resource`
--

LOCK TABLES `resource` WRITE;
/*!40000 ALTER TABLE `resource` DISABLE KEYS */;
INSERT INTO `resource` VALUES (1,'washingmachine',7),(2,'washingmachine',3),(3,'washingmachine',2),(4,'washingmachine',1),(5,'washingmachine',0),(6,'washingmachine',1),(7,'washingmachine',3),(8,'washingmachine',1),(9,'washingmachine',1),(10,'washingmachine',3),(11,'partyhall',2),(12,'partyhall',0),(13,'partyhall',1),(14,'partyhall',2),(15,'partyhall',3),(16,'partyhall',0),(17,'partyhall',0),(18,'partyhall',1),(19,'partyhall',2),(20,'partyhall',0),(21,'parkingspace',1),(22,'parkingspace',0),(23,'parkingspace',0),(24,'parkingspace',2),(25,'parkingspace',0),(26,'parkingspace',2),(27,'parkingspace',0),(28,'parkingspace',0),(29,'parkingspace',0),(30,'parkingspace',1);
/*!40000 ALTER TABLE `resource` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database '2SemesterEksamen'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-05-09 11:09:07
