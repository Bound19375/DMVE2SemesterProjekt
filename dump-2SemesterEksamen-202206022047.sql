-- MySQL dump 10.13  Distrib 5.5.62, for Win64 (AMD64)
--
-- Host: bound1937.ddns.net    Database: 2SemesterEksamen
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
INSERT INTO `account` VALUES ('A','¸¥|ì?áÅ§‘˙È/˝','admin','11111111','AA','BB','internal','2022-05-09 11:00:11'),('Bo','¸¥|ì?áÅ§‘˙È/˝','resident','11111111','Bo','Bosen','youth','2022-05-09 11:00:11'),('BoundSoul','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Simon','Klausen','youth','2022-05-09 11:00:11'),('dda','U)˙iæ:µΩâR\0LOŒZ','waitlist','11111111','Das','Dausen','normal','2022-05-09 11:00:11'),('D√∏medk√∏lle','¸¥|ì?áÅ§‘˙È/˝','resident','88888888','Dolf','Knudsen','normal','2022-05-09 11:00:11'),('hhh','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','DAAA','ddd','normal','2022-05-09 11:00:11'),('Jan','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Bo','Janden','senior','2022-05-09 11:00:11'),('jkj','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Jesper','Flimsen','youth','2022-05-09 11:00:11'),('Knudsen48','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Knud','Knudsen','normal','2022-05-09 11:00:11'),('Lars','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Lars','Snasken','youth','2022-05-09 11:00:11'),('normie420','Tπ)S3ê®gTmÀ@Dıe5','waitlist','11111111','Tank','Dasken','senior','2022-05-09 11:00:11'),('oleee400','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Ole','Monsen','normal','2022-05-09 11:00:11'),('Ovemove','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Ove','Musken','senior','2022-05-09 11:00:11'),('S','¸¥|ì?áÅ§‘˙È/˝','secretary','11111111','Sam','Ausen','internal','2022-05-09 11:00:11'),('TimIsCool','¸¥|ì?áÅ§‘˙È/˝','waitlist','11111111','Tim','Dusen','normal','2022-05-09 11:00:11');
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
) ENGINE=InnoDB AUTO_INCREMENT=74 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_resource_reservations`
--

LOCK TABLES `account_resource_reservations` WRITE;
/*!40000 ALTER TABLE `account_resource_reservations` DISABLE KEYS */;
INSERT INTO `account_resource_reservations` VALUES (73,'Bo',10,'2022-05-09 16:05:00','2022-05-09 18:05:00');
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
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `housing`
--

LOCK TABLES `housing` WRITE;
/*!40000 ALTER TABLE `housing` DISABLE KEYS */;
INSERT INTO `housing` VALUES (18,'normal',120,9000,'HejGade 4',7100),(19,'youth',50,4000,'Dolf All√© 4',7100),(20,'normal',200,10000,'Smil All√©',7100);
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
  UNIQUE KEY `housing_account_UN` (`account_username`),
  CONSTRAINT `housing_residents_FK` FOREIGN KEY (`account_username`) REFERENCES `account` (`username`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `housing_residents_FK_1` FOREIGN KEY (`housing_id`) REFERENCES `housing` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `housing_account`
--

LOCK TABLES `housing_account` WRITE;
/*!40000 ALTER TABLE `housing_account` DISABLE KEYS */;
INSERT INTO `housing_account` VALUES (18,'D√∏medk√∏lle','2022-05-09 15:45:02'),(19,'Bo','2022-05-09 15:48:56');
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
INSERT INTO `locality` VALUES (1000,'K√∏benhavn K'),(1500,'K√∏benhavn V'),(1835,'Frederiksberg C'),(2000,'Frederiksberg'),(2100,'K√∏benhavn √ò'),(2150,'Nordhavn'),(2200,'K√∏benhavn N'),(2300,'K√∏benhavn S'),(2400,'K√∏benhavn NV'),(2450,'K√∏benhavn SV'),(2500,'Valby'),(2600,'Glostrup'),(2605,'Br√∏ndby'),(2610,'R√∏dovre'),(2620,'Albertslund'),(2625,'Vallensb√¶k'),(2630,'Taastrup'),(2635,'Ish√∏j'),(2640,'Hedehusene'),(2650,'Hvidovre'),(2660,'Br√∏ndby Strand'),(2665,'Vallensb√¶k Strand'),(2670,'Greve'),(2680,'Solr√∏d Strand'),(2690,'Karlslunde'),(2700,'Br√∏nsh√∏j'),(2720,'Vanl√∏se'),(2730,'Herlev'),(2740,'Skovlunde'),(2750,'Ballerup'),(2760,'M√•l√∏v'),(2765,'Sm√∏rum'),(2770,'Kastrup'),(2791,'Drag√∏r'),(2800,'Kongens Lyngby'),(2820,'Gentofte'),(2830,'Virum'),(2840,'Holte'),(2850,'N√¶rum'),(2860,'S√∏borg'),(2870,'Dysseg√•rd'),(2880,'Bagsv√¶rd'),(2900,'Hellerup'),(2920,'Charlottenlund'),(2930,'Klampenborg'),(2942,'Skodsborg'),(2950,'Vedb√¶k'),(2960,'Rungsted Kyst'),(2970,'H√∏rsholm'),(2980,'Kokkedal'),(2990,'Niv√•'),(3000,'Helsing√∏r'),(3050,'Humleb√¶k'),(3060,'Esperg√¶rde'),(3070,'Snekkersten'),(3080,'Tik√∏b'),(3100,'Hornb√¶k'),(3120,'Dronningm√∏lle'),(3140,'√Ölsg√•rde'),(3150,'Helleb√¶k'),(3200,'Helsinge'),(3210,'Vejby'),(3220,'Tisvildeleje'),(3230,'Gr√¶sted'),(3250,'Gilleleje'),(3300,'Frederiksv√¶rk'),(3310,'√òlsted'),(3320,'Sk√¶vinge'),(3330,'G√∏rl√∏se'),(3360,'Liseleje'),(3370,'Melby'),(3390,'Hundested'),(3400,'Hiller√∏d'),(3450,'Aller√∏d'),(3460,'Birker√∏d'),(3480,'Fredensborg'),(3490,'Kvistg√•rd'),(3500,'V√¶rl√∏se'),(3520,'Farum'),(3540,'Lynge'),(3550,'Slangerup'),(3600,'Frederikssund'),(3630,'J√¶gerspris'),(3650,'√òlstykke'),(3660,'Stenl√∏se'),(3670,'Veks√∏ Sj√¶lland'),(3700,'R√∏nne'),(3720,'Aakirkeby'),(3730,'Nex√∏'),(3740,'Svaneke'),(3751,'√òstermarie'),(3760,'Gudhjem'),(3770,'Allinge'),(3782,'Klemensker'),(3790,'Hasle'),(4000,'Roskilde'),(4030,'Tune'),(4040,'Jyllinge'),(4050,'Skibby'),(4060,'Kirke S√•by'),(4070,'Kirke Hyllinge'),(4130,'Viby Sj√¶lland'),(4140,'Borup'),(4160,'Herlufmagle'),(4190,'Munke Bjergby'),(4200,'Slagelse'),(4220,'Kors√∏r'),(4230,'Sk√¶lsk√∏r'),(4241,'Vemmelev'),(4242,'Boeslunde'),(4243,'Rude'),(4244,'Agers√∏'),(4245,'Om√∏'),(4250,'Fuglebjerg'),(4261,'Dalmose'),(4262,'Sandved'),(4270,'H√∏ng'),(4281,'G√∏rlev'),(4291,'Ruds Vedby'),(4293,'Dianalund'),(4295,'Stenlille'),(4296,'Nyrup'),(4300,'Holb√¶k'),(4305,'Or√∏'),(4320,'Lejre'),(4340,'T√∏ll√∏se'),(4350,'Ugerl√∏se'),(4390,'Vipper√∏d'),(4400,'Kalundborg'),(4420,'Regstrup'),(4440,'M√∏rk√∏v'),(4450,'Jyderup'),(4460,'Snertinge'),(4470,'Sveb√∏lle'),(4480,'Store Fuglede'),(4490,'Jerslev Sj√¶lland'),(4500,'Nyk√∏bing Sj'),(4520,'Svinninge'),(4532,'Gislinge'),(4534,'H√∏rve'),(4540,'F√•revejle'),(4550,'Asn√¶s'),(4560,'Vig'),(4571,'Grevinge'),(4572,'N√∏rre Asmindrup'),(4573,'H√∏jby'),(4581,'R√∏rvig'),(4583,'Sj√¶llands Odde'),(4591,'F√∏llenslev'),(4592,'Sejer√∏'),(4593,'Eskebjerg'),(4600,'K√∏ge'),(4621,'Gadstrup'),(4622,'Havdrup'),(4623,'Lille Skensved'),(4632,'Bj√¶verskov'),(4640,'Faxe'),(4652,'H√•rlev'),(4653,'Karise'),(4654,'Faxe Ladeplads'),(4660,'Store Heddinge'),(4671,'Str√∏by'),(4672,'Klippinge'),(4673,'R√∏dvig Stevns'),(4681,'Herf√∏lge'),(4683,'R√∏nnede'),(4684,'Holmegaard'),(4700,'N√¶stved'),(4720,'Pr√¶st√∏'),(4733,'Tappern√∏je'),(4735,'Mern'),(4736,'Karreb√¶ksminde'),(4750,'Lundby'),(4760,'Vordingborg'),(4771,'Kalvehave'),(4772,'Langeb√¶k'),(4773,'Stensved'),(4780,'Stege'),(4791,'Borre'),(4792,'Askeby'),(4793,'Bog√∏ By'),(4800,'Nyk√∏bing F'),(4840,'N√∏rre Alslev'),(4850,'Stubbek√∏bing'),(4862,'Guldborg'),(4863,'Eskilstrup'),(4871,'Horbelev'),(4872,'Idestrup'),(4873,'V√¶ggerl√∏se'),(4874,'Gedser'),(4880,'Nysted'),(4891,'Toreby L'),(4892,'Kettinge'),(4894,'√òster Ulslev'),(4895,'Errindlev'),(4900,'Nakskov'),(4912,'Harpelunde'),(4913,'Horslunde'),(4920,'S√∏llested'),(4930,'Maribo'),(4941,'Bandholm'),(4942,'Ask√∏'),(4943,'Torrig L'),(4944,'Fej√∏'),(4945,'Fem√∏'),(4951,'N√∏rreballe'),(4952,'Stokkemarke'),(4953,'Vesterborg'),(4960,'Holeby'),(4970,'R√∏dby'),(4983,'Dannemare'),(4990,'Saksk√∏bing'),(5000,'Odense C'),(5200,'Odense V'),(5210,'Odense NV'),(5220,'Odense S√ò'),(5230,'Odense M'),(5240,'Odense N√ò'),(5250,'Odense SV'),(5260,'Odense S'),(5270,'Odense N'),(5290,'Marslev'),(5300,'Kerteminde'),(5320,'Agedrup'),(5330,'Munkebo'),(5350,'Rynkeby'),(5370,'Mesinge'),(5380,'Dalby'),(5390,'Martofte'),(5400,'Bogense'),(5450,'Otterup'),(5462,'Morud'),(5463,'Harndrup'),(5464,'Brenderup Fyn'),(5466,'Asperup'),(5471,'S√∏nders√∏'),(5474,'Veflinge'),(5485,'Skamby'),(5491,'Blommenslyst'),(5492,'Vissenbjerg'),(5500,'Middelfart'),(5540,'Ullerslev'),(5550,'Langeskov'),(5560,'Aarup'),(5580,'N√∏rre Aaby'),(5591,'Gelsted'),(5592,'Ejby'),(5600,'Faaborg'),(5601,'Ly√∏'),(5602,'Avernak√∏'),(5603,'Bj√∏rn√∏'),(5610,'Assens'),(5620,'Glamsbjerg'),(5631,'Ebberup'),(5642,'Millinge'),(5672,'Broby'),(5683,'Haarby'),(5690,'Tommerup'),(5700,'Svendborg'),(5750,'Ringe'),(5762,'Vester Skerninge'),(5771,'Stenstrup'),(5772,'Kv√¶rndrup'),(5792,'√Örslev'),(5800,'Nyborg'),(5853,'√òrb√¶k'),(5854,'Gislev'),(5856,'Ryslinge'),(5863,'Ferritslev Fyn'),(5871,'Fr√∏rup'),(5874,'Hesselager'),(5881,'Sk√•rup Fyn'),(5882,'Vejstrup'),(5883,'Oure'),(5884,'Gudme'),(5892,'Gudbjerg Sydfyn'),(5900,'Rudk√∏bing'),(5932,'Humble'),(5935,'Bagenkop'),(5943,'Stryn√∏'),(5953,'Tranek√¶r'),(5960,'Marstal'),(5965,'Birkholm'),(5970,'√Ür√∏sk√∏bing'),(5985,'S√∏by √Ür√∏'),(6000,'Kolding'),(6040,'Egtved'),(6051,'Almind'),(6052,'Viuf'),(6064,'Jordrup'),(6070,'Christiansfeld'),(6091,'Bjert'),(6092,'S√∏nder Stenderup'),(6093,'Sj√∏lund'),(6094,'Hejls'),(6100,'Haderslev'),(6200,'Aabenraa'),(6210,'Bars√∏'),(6230,'R√∏dekro'),(6240,'L√∏gumkloster'),(6261,'Bredebro'),(6270,'T√∏nder'),(6280,'H√∏jer'),(6300,'Gr√•sten'),(6310,'Broager'),(6320,'Egernsund'),(6330,'Padborg'),(6340,'Krus√•'),(6360,'Tinglev'),(6372,'Bylderup-Bov'),(6392,'Bolderslev'),(6400,'S√∏nderborg'),(6430,'Nordborg'),(6440,'Augustenborg'),(6470,'Sydals'),(6500,'Vojens'),(6510,'Gram'),(6520,'Toftlund'),(6534,'Agerskov'),(6535,'Branderup J'),(6541,'Bevtoft'),(6560,'Sommersted'),(6580,'Vamdrup'),(6600,'Vejen'),(6621,'Gesten'),(6622,'B√¶kke'),(6623,'Vorbasse'),(6630,'R√∏dding'),(6640,'Lunderskov'),(6650,'Br√∏rup'),(6660,'Lintrup'),(6670,'Holsted'),(6682,'Hovborg'),(6683,'F√∏vling'),(6690,'G√∏rding'),(6700,'Esbjerg'),(6705,'Esbjerg √ò'),(6710,'Esbjerg V'),(6715,'Esbjerg N'),(6720,'Fan√∏'),(6731,'Tj√¶reborg'),(6740,'Bramming'),(6752,'Glejbjerg'),(6753,'Agerb√¶k'),(6760,'Ribe'),(6771,'Gredstedbro'),(6780,'Sk√¶rb√¶k'),(6792,'R√∏m√∏'),(6800,'Varde'),(6818,'√Örre'),(6823,'Ansager'),(6830,'N√∏rre Nebel'),(6840,'Oksb√∏l'),(6851,'Janderup Vestj'),(6852,'Billum'),(6853,'Vejers Strand'),(6854,'Henne'),(6855,'Outrup'),(6857,'Bl√•vand'),(6862,'Tistrup'),(6870,'√òlgod'),(6880,'Tarm'),(6893,'Hemmet'),(6900,'Skjern'),(6920,'Videb√¶k'),(6933,'Kib√¶k'),(6940,'Lem St'),(6950,'Ringk√∏bing'),(6960,'Hvide Sande'),(6971,'Spjald'),(6973,'√òrnh√∏j'),(6980,'Tim'),(6990,'Ulfborg'),(7000,'Fredericia'),(7080,'B√∏rkop'),(7100,'Vejle'),(7120,'Vejle √òst'),(7130,'Juelsminde'),(7140,'Stouby'),(7150,'Barrit'),(7160,'T√∏rring'),(7171,'Uldum'),(7173,'Vonge'),(7182,'Bredsten'),(7183,'Randb√∏l'),(7184,'Vandel'),(7190,'Billund'),(7200,'Grindsted'),(7250,'Hejnsvig'),(7260,'S√∏nder Omme'),(7270,'Stakroge'),(7280,'S√∏nder Felding'),(7300,'Jelling'),(7321,'Gadbjerg'),(7323,'Give'),(7330,'Brande'),(7361,'Ejstrupholm'),(7362,'Hampen'),(7400,'Herning'),(7430,'Ikast'),(7441,'Bording'),(7442,'Engesvang'),(7451,'Sunds'),(7470,'Karup J'),(7480,'Vildbjerg'),(7490,'Aulum'),(7500,'Holstebro'),(7540,'Haderup'),(7550,'S√∏rvad'),(7560,'Hjerm'),(7570,'Vemb'),(7600,'Struer'),(7620,'Lemvig'),(7650,'B√∏vlingbjerg'),(7660,'B√¶kmarksbro'),(7673,'Harbo√∏re'),(7680,'Thybor√∏n'),(7700,'Thisted'),(7730,'Hanstholm'),(7741,'Fr√∏strup'),(7742,'Vesl√∏s'),(7752,'Snedsted'),(7755,'Bedsted Thy'),(7760,'Hurup Thy'),(7770,'Vestervig'),(7790,'Thyholm'),(7800,'Skive'),(7830,'Vinderup'),(7840,'H√∏jslev'),(7850,'Stoholm Jyll'),(7860,'Sp√∏ttrup'),(7870,'Roslev'),(7884,'Fur'),(7900,'Nyk√∏bing M'),(7950,'Erslev'),(7960,'Karby'),(7970,'Redsted M'),(7980,'Vils'),(7990,'√òster Assels'),(8000,'Aarhus C'),(8200,'Aarhus N'),(8210,'Aarhus V'),(8220,'Brabrand'),(8230,'√Öbyh√∏j'),(8240,'Risskov'),(8250,'Eg√•'),(8260,'Viby J'),(8270,'H√∏jbjerg'),(8300,'Odder'),(8305,'Sams√∏'),(8310,'Tranbjerg J'),(8320,'M√•rslet'),(8330,'Beder'),(8340,'Malling'),(8350,'Hundslund'),(8355,'Solbjerg'),(8361,'Hasselager'),(8362,'H√∏rning'),(8370,'Hadsten'),(8380,'Trige'),(8381,'Tilst'),(8382,'Hinnerup'),(8400,'Ebeltoft'),(8410,'R√∏nde'),(8420,'Knebel'),(8444,'Balle'),(8450,'Hammel'),(8462,'Harlev J'),(8464,'Galten'),(8471,'Sabro'),(8472,'Sporup'),(8500,'Grenaa'),(8520,'Lystrup'),(8530,'Hjortsh√∏j'),(8541,'Sk√∏dstrup'),(8543,'Hornslet'),(8544,'M√∏rke'),(8550,'Ryomg√•rd'),(8560,'Kolind'),(8570,'Trustrup'),(8581,'Nimtofte'),(8585,'Glesborg'),(8586,'√òrum Djurs'),(8592,'Anholt'),(8600,'Silkeborg'),(8620,'Kjellerup'),(8632,'Lemming'),(8641,'Sorring'),(8643,'Ans By'),(8653,'Them'),(8654,'Bryrup'),(8660,'Skanderborg'),(8670,'L√•sby'),(8680,'Ry'),(8700,'Horsens'),(8721,'Daug√•rd'),(8722,'Hedensted'),(8723,'L√∏sning'),(8732,'Hovedg√•rd'),(8740,'Br√¶dstrup'),(8751,'Gedved'),(8752,'√òstbirk'),(8762,'Flemming'),(8763,'Rask M√∏lle'),(8765,'Klovborg'),(8766,'N√∏rre Snede'),(8781,'Stenderup'),(8783,'Hornsyld'),(8789,'Endelave'),(8799,'Tun√∏'),(8800,'Viborg'),(8830,'Tjele'),(8831,'L√∏gstrup'),(8832,'Skals'),(8840,'R√∏dk√¶rsbro'),(8850,'Bjerringbro'),(8860,'Ulstrup'),(8870,'Lang√•'),(8881,'Thors√∏'),(8882,'F√•rvang'),(8883,'Gjern'),(8900,'Randers C'),(8920,'Randers NV'),(8930,'Randers N√ò'),(8940,'Randers SV'),(8950,'√òrsted'),(8960,'Randers S√ò'),(8961,'Alling√•bro'),(8963,'Auning'),(8970,'Havndal'),(8981,'Spentrup'),(8983,'Gjerlev J'),(8990,'F√•rup'),(9000,'Aalborg'),(9200,'Aalborg SV'),(9210,'Aalborg S√ò'),(9220,'Aalborg √òst'),(9230,'Svenstrup J'),(9240,'Nibe'),(9260,'Gistrup'),(9270,'Klarup'),(9280,'Storvorde'),(9293,'Kongerslev'),(9300,'S√¶by'),(9310,'Vodskov'),(9320,'Hjallerup'),(9330,'Dronninglund'),(9340,'Asaa'),(9352,'Dybvad'),(9362,'Gandrup'),(9370,'Hals'),(9380,'Vestbjerg'),(9381,'Sulsted'),(9382,'Tylstrup'),(9400,'N√∏rresundby'),(9430,'Vadum'),(9440,'Aabybro'),(9460,'Brovst'),(9480,'L√∏kken'),(9490,'Pandrup'),(9492,'Blokhus'),(9493,'Saltum'),(9500,'Hobro'),(9510,'Arden'),(9520,'Sk√∏rping'),(9530,'St√∏vring'),(9541,'Suldrup'),(9550,'Mariager'),(9560,'Hadsund'),(9574,'B√¶lum'),(9575,'Terndrup'),(9600,'Aars'),(9610,'N√∏rager'),(9620,'Aalestrup'),(9631,'Gedsted'),(9632,'M√∏ldrup'),(9640,'Fars√∏'),(9670,'L√∏gst√∏r'),(9681,'Ranum'),(9690,'Fjerritslev'),(9700,'Br√∏nderslev'),(9740,'Jerslev J'),(9750,'√òstervr√•'),(9760,'Vr√•'),(9800,'Hj√∏rring'),(9830,'T√•rs'),(9850,'Hirtshals'),(9870,'Sindal'),(9881,'Bindslev'),(9900,'Frederikshavn'),(9940,'L√¶s√∏'),(9970,'Strandby'),(9981,'Jerup'),(9982,'√Ölb√¶k'),(9990,'Skagen');
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
INSERT INTO `resource` VALUES (1,'washingmachine',7),(2,'washingmachine',3),(3,'washingmachine',2),(4,'washingmachine',1),(5,'washingmachine',0),(6,'washingmachine',1),(7,'washingmachine',3),(8,'washingmachine',1),(9,'washingmachine',1),(10,'washingmachine',4),(11,'partyhall',2),(12,'partyhall',0),(13,'partyhall',1),(14,'partyhall',2),(15,'partyhall',3),(16,'partyhall',0),(17,'partyhall',0),(18,'partyhall',1),(19,'partyhall',2),(20,'partyhall',0),(21,'parkingspace',1),(22,'parkingspace',0),(23,'parkingspace',0),(24,'parkingspace',2),(25,'parkingspace',0),(26,'parkingspace',2),(27,'parkingspace',0),(28,'parkingspace',0),(29,'parkingspace',0),(30,'parkingspace',1);
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

-- Dump completed on 2022-06-02 20:47:36
