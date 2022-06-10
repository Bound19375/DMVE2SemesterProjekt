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
INSERT INTO `account` VALUES ('A','��|�?������/�','admin','11111111','AA','BB','internal','2022-05-09 11:00:11'),('Bo','��|�?������/�','resident','11111111','Bo','Bosen','youth','2022-05-09 11:00:11'),('BoundSoul','��|�?������/�','waitlist','11111111','Simon','Klausen','youth','2022-05-09 11:00:11'),('dda','U)�i�:���R\0LO�Z','waitlist','11111111','Das','Dausen','normal','2022-05-09 11:00:11'),('Dømedkølle','��|�?������/�','resident','88888888','Dolf','Knudsen','normal','2022-05-09 11:00:11'),('hhh','��|�?������/�','waitlist','11111111','DAAA','ddd','normal','2022-05-09 11:00:11'),('Jan','��|�?������/�','waitlist','11111111','Bo','Janden','senior','2022-05-09 11:00:11'),('jkj','��|�?������/�','waitlist','11111111','Jesper','Flimsen','youth','2022-05-09 11:00:11'),('Knudsen48','��|�?������/�','waitlist','11111111','Knud','Knudsen','normal','2022-05-09 11:00:11'),('Lars','��|�?������/�','waitlist','11111111','Lars','Snasken','youth','2022-05-09 11:00:11'),('normie420','T�)S3��gTm�@D�e5','waitlist','11111111','Tank','Dasken','senior','2022-05-09 11:00:11'),('oleee400','��|�?������/�','waitlist','11111111','Ole','Monsen','normal','2022-05-09 11:00:11'),('Ovemove','��|�?������/�','waitlist','11111111','Ove','Musken','senior','2022-05-09 11:00:11'),('S','��|�?������/�','secretary','11111111','Sam','Ausen','internal','2022-05-09 11:00:11'),('TimIsCool','��|�?������/�','waitlist','11111111','Tim','Dusen','normal','2022-05-09 11:00:11');
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
INSERT INTO `housing` VALUES (18,'normal',120,9000,'HejGade 4',7100),(19,'youth',50,4000,'Dolf Allé 4',7100),(20,'normal',200,10000,'Smil Allé',7100);
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
INSERT INTO `housing_account` VALUES (18,'Dømedkølle','2022-05-09 15:45:02'),(19,'Bo','2022-05-09 15:48:56');
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
INSERT INTO `locality` VALUES (1000,'København K'),(1500,'København V'),(1835,'Frederiksberg C'),(2000,'Frederiksberg'),(2100,'København Ø'),(2150,'Nordhavn'),(2200,'København N'),(2300,'København S'),(2400,'København NV'),(2450,'København SV'),(2500,'Valby'),(2600,'Glostrup'),(2605,'Brøndby'),(2610,'Rødovre'),(2620,'Albertslund'),(2625,'Vallensbæk'),(2630,'Taastrup'),(2635,'Ishøj'),(2640,'Hedehusene'),(2650,'Hvidovre'),(2660,'Brøndby Strand'),(2665,'Vallensbæk Strand'),(2670,'Greve'),(2680,'Solrød Strand'),(2690,'Karlslunde'),(2700,'Brønshøj'),(2720,'Vanløse'),(2730,'Herlev'),(2740,'Skovlunde'),(2750,'Ballerup'),(2760,'Måløv'),(2765,'Smørum'),(2770,'Kastrup'),(2791,'Dragør'),(2800,'Kongens Lyngby'),(2820,'Gentofte'),(2830,'Virum'),(2840,'Holte'),(2850,'Nærum'),(2860,'Søborg'),(2870,'Dyssegård'),(2880,'Bagsværd'),(2900,'Hellerup'),(2920,'Charlottenlund'),(2930,'Klampenborg'),(2942,'Skodsborg'),(2950,'Vedbæk'),(2960,'Rungsted Kyst'),(2970,'Hørsholm'),(2980,'Kokkedal'),(2990,'Nivå'),(3000,'Helsingør'),(3050,'Humlebæk'),(3060,'Espergærde'),(3070,'Snekkersten'),(3080,'Tikøb'),(3100,'Hornbæk'),(3120,'Dronningmølle'),(3140,'Ålsgårde'),(3150,'Hellebæk'),(3200,'Helsinge'),(3210,'Vejby'),(3220,'Tisvildeleje'),(3230,'Græsted'),(3250,'Gilleleje'),(3300,'Frederiksværk'),(3310,'Ølsted'),(3320,'Skævinge'),(3330,'Gørløse'),(3360,'Liseleje'),(3370,'Melby'),(3390,'Hundested'),(3400,'Hillerød'),(3450,'Allerød'),(3460,'Birkerød'),(3480,'Fredensborg'),(3490,'Kvistgård'),(3500,'Værløse'),(3520,'Farum'),(3540,'Lynge'),(3550,'Slangerup'),(3600,'Frederikssund'),(3630,'Jægerspris'),(3650,'Ølstykke'),(3660,'Stenløse'),(3670,'Veksø Sjælland'),(3700,'Rønne'),(3720,'Aakirkeby'),(3730,'Nexø'),(3740,'Svaneke'),(3751,'Østermarie'),(3760,'Gudhjem'),(3770,'Allinge'),(3782,'Klemensker'),(3790,'Hasle'),(4000,'Roskilde'),(4030,'Tune'),(4040,'Jyllinge'),(4050,'Skibby'),(4060,'Kirke Såby'),(4070,'Kirke Hyllinge'),(4130,'Viby Sjælland'),(4140,'Borup'),(4160,'Herlufmagle'),(4190,'Munke Bjergby'),(4200,'Slagelse'),(4220,'Korsør'),(4230,'Skælskør'),(4241,'Vemmelev'),(4242,'Boeslunde'),(4243,'Rude'),(4244,'Agersø'),(4245,'Omø'),(4250,'Fuglebjerg'),(4261,'Dalmose'),(4262,'Sandved'),(4270,'Høng'),(4281,'Gørlev'),(4291,'Ruds Vedby'),(4293,'Dianalund'),(4295,'Stenlille'),(4296,'Nyrup'),(4300,'Holbæk'),(4305,'Orø'),(4320,'Lejre'),(4340,'Tølløse'),(4350,'Ugerløse'),(4390,'Vipperød'),(4400,'Kalundborg'),(4420,'Regstrup'),(4440,'Mørkøv'),(4450,'Jyderup'),(4460,'Snertinge'),(4470,'Svebølle'),(4480,'Store Fuglede'),(4490,'Jerslev Sjælland'),(4500,'Nykøbing Sj'),(4520,'Svinninge'),(4532,'Gislinge'),(4534,'Hørve'),(4540,'Fårevejle'),(4550,'Asnæs'),(4560,'Vig'),(4571,'Grevinge'),(4572,'Nørre Asmindrup'),(4573,'Højby'),(4581,'Rørvig'),(4583,'Sjællands Odde'),(4591,'Føllenslev'),(4592,'Sejerø'),(4593,'Eskebjerg'),(4600,'Køge'),(4621,'Gadstrup'),(4622,'Havdrup'),(4623,'Lille Skensved'),(4632,'Bjæverskov'),(4640,'Faxe'),(4652,'Hårlev'),(4653,'Karise'),(4654,'Faxe Ladeplads'),(4660,'Store Heddinge'),(4671,'Strøby'),(4672,'Klippinge'),(4673,'Rødvig Stevns'),(4681,'Herfølge'),(4683,'Rønnede'),(4684,'Holmegaard'),(4700,'Næstved'),(4720,'Præstø'),(4733,'Tappernøje'),(4735,'Mern'),(4736,'Karrebæksminde'),(4750,'Lundby'),(4760,'Vordingborg'),(4771,'Kalvehave'),(4772,'Langebæk'),(4773,'Stensved'),(4780,'Stege'),(4791,'Borre'),(4792,'Askeby'),(4793,'Bogø By'),(4800,'Nykøbing F'),(4840,'Nørre Alslev'),(4850,'Stubbekøbing'),(4862,'Guldborg'),(4863,'Eskilstrup'),(4871,'Horbelev'),(4872,'Idestrup'),(4873,'Væggerløse'),(4874,'Gedser'),(4880,'Nysted'),(4891,'Toreby L'),(4892,'Kettinge'),(4894,'Øster Ulslev'),(4895,'Errindlev'),(4900,'Nakskov'),(4912,'Harpelunde'),(4913,'Horslunde'),(4920,'Søllested'),(4930,'Maribo'),(4941,'Bandholm'),(4942,'Askø'),(4943,'Torrig L'),(4944,'Fejø'),(4945,'Femø'),(4951,'Nørreballe'),(4952,'Stokkemarke'),(4953,'Vesterborg'),(4960,'Holeby'),(4970,'Rødby'),(4983,'Dannemare'),(4990,'Sakskøbing'),(5000,'Odense C'),(5200,'Odense V'),(5210,'Odense NV'),(5220,'Odense SØ'),(5230,'Odense M'),(5240,'Odense NØ'),(5250,'Odense SV'),(5260,'Odense S'),(5270,'Odense N'),(5290,'Marslev'),(5300,'Kerteminde'),(5320,'Agedrup'),(5330,'Munkebo'),(5350,'Rynkeby'),(5370,'Mesinge'),(5380,'Dalby'),(5390,'Martofte'),(5400,'Bogense'),(5450,'Otterup'),(5462,'Morud'),(5463,'Harndrup'),(5464,'Brenderup Fyn'),(5466,'Asperup'),(5471,'Søndersø'),(5474,'Veflinge'),(5485,'Skamby'),(5491,'Blommenslyst'),(5492,'Vissenbjerg'),(5500,'Middelfart'),(5540,'Ullerslev'),(5550,'Langeskov'),(5560,'Aarup'),(5580,'Nørre Aaby'),(5591,'Gelsted'),(5592,'Ejby'),(5600,'Faaborg'),(5601,'Lyø'),(5602,'Avernakø'),(5603,'Bjørnø'),(5610,'Assens'),(5620,'Glamsbjerg'),(5631,'Ebberup'),(5642,'Millinge'),(5672,'Broby'),(5683,'Haarby'),(5690,'Tommerup'),(5700,'Svendborg'),(5750,'Ringe'),(5762,'Vester Skerninge'),(5771,'Stenstrup'),(5772,'Kværndrup'),(5792,'Årslev'),(5800,'Nyborg'),(5853,'Ørbæk'),(5854,'Gislev'),(5856,'Ryslinge'),(5863,'Ferritslev Fyn'),(5871,'Frørup'),(5874,'Hesselager'),(5881,'Skårup Fyn'),(5882,'Vejstrup'),(5883,'Oure'),(5884,'Gudme'),(5892,'Gudbjerg Sydfyn'),(5900,'Rudkøbing'),(5932,'Humble'),(5935,'Bagenkop'),(5943,'Strynø'),(5953,'Tranekær'),(5960,'Marstal'),(5965,'Birkholm'),(5970,'Ærøskøbing'),(5985,'Søby Ærø'),(6000,'Kolding'),(6040,'Egtved'),(6051,'Almind'),(6052,'Viuf'),(6064,'Jordrup'),(6070,'Christiansfeld'),(6091,'Bjert'),(6092,'Sønder Stenderup'),(6093,'Sjølund'),(6094,'Hejls'),(6100,'Haderslev'),(6200,'Aabenraa'),(6210,'Barsø'),(6230,'Rødekro'),(6240,'Løgumkloster'),(6261,'Bredebro'),(6270,'Tønder'),(6280,'Højer'),(6300,'Gråsten'),(6310,'Broager'),(6320,'Egernsund'),(6330,'Padborg'),(6340,'Kruså'),(6360,'Tinglev'),(6372,'Bylderup-Bov'),(6392,'Bolderslev'),(6400,'Sønderborg'),(6430,'Nordborg'),(6440,'Augustenborg'),(6470,'Sydals'),(6500,'Vojens'),(6510,'Gram'),(6520,'Toftlund'),(6534,'Agerskov'),(6535,'Branderup J'),(6541,'Bevtoft'),(6560,'Sommersted'),(6580,'Vamdrup'),(6600,'Vejen'),(6621,'Gesten'),(6622,'Bække'),(6623,'Vorbasse'),(6630,'Rødding'),(6640,'Lunderskov'),(6650,'Brørup'),(6660,'Lintrup'),(6670,'Holsted'),(6682,'Hovborg'),(6683,'Føvling'),(6690,'Gørding'),(6700,'Esbjerg'),(6705,'Esbjerg Ø'),(6710,'Esbjerg V'),(6715,'Esbjerg N'),(6720,'Fanø'),(6731,'Tjæreborg'),(6740,'Bramming'),(6752,'Glejbjerg'),(6753,'Agerbæk'),(6760,'Ribe'),(6771,'Gredstedbro'),(6780,'Skærbæk'),(6792,'Rømø'),(6800,'Varde'),(6818,'Årre'),(6823,'Ansager'),(6830,'Nørre Nebel'),(6840,'Oksbøl'),(6851,'Janderup Vestj'),(6852,'Billum'),(6853,'Vejers Strand'),(6854,'Henne'),(6855,'Outrup'),(6857,'Blåvand'),(6862,'Tistrup'),(6870,'Ølgod'),(6880,'Tarm'),(6893,'Hemmet'),(6900,'Skjern'),(6920,'Videbæk'),(6933,'Kibæk'),(6940,'Lem St'),(6950,'Ringkøbing'),(6960,'Hvide Sande'),(6971,'Spjald'),(6973,'Ørnhøj'),(6980,'Tim'),(6990,'Ulfborg'),(7000,'Fredericia'),(7080,'Børkop'),(7100,'Vejle'),(7120,'Vejle Øst'),(7130,'Juelsminde'),(7140,'Stouby'),(7150,'Barrit'),(7160,'Tørring'),(7171,'Uldum'),(7173,'Vonge'),(7182,'Bredsten'),(7183,'Randbøl'),(7184,'Vandel'),(7190,'Billund'),(7200,'Grindsted'),(7250,'Hejnsvig'),(7260,'Sønder Omme'),(7270,'Stakroge'),(7280,'Sønder Felding'),(7300,'Jelling'),(7321,'Gadbjerg'),(7323,'Give'),(7330,'Brande'),(7361,'Ejstrupholm'),(7362,'Hampen'),(7400,'Herning'),(7430,'Ikast'),(7441,'Bording'),(7442,'Engesvang'),(7451,'Sunds'),(7470,'Karup J'),(7480,'Vildbjerg'),(7490,'Aulum'),(7500,'Holstebro'),(7540,'Haderup'),(7550,'Sørvad'),(7560,'Hjerm'),(7570,'Vemb'),(7600,'Struer'),(7620,'Lemvig'),(7650,'Bøvlingbjerg'),(7660,'Bækmarksbro'),(7673,'Harboøre'),(7680,'Thyborøn'),(7700,'Thisted'),(7730,'Hanstholm'),(7741,'Frøstrup'),(7742,'Vesløs'),(7752,'Snedsted'),(7755,'Bedsted Thy'),(7760,'Hurup Thy'),(7770,'Vestervig'),(7790,'Thyholm'),(7800,'Skive'),(7830,'Vinderup'),(7840,'Højslev'),(7850,'Stoholm Jyll'),(7860,'Spøttrup'),(7870,'Roslev'),(7884,'Fur'),(7900,'Nykøbing M'),(7950,'Erslev'),(7960,'Karby'),(7970,'Redsted M'),(7980,'Vils'),(7990,'Øster Assels'),(8000,'Aarhus C'),(8200,'Aarhus N'),(8210,'Aarhus V'),(8220,'Brabrand'),(8230,'Åbyhøj'),(8240,'Risskov'),(8250,'Egå'),(8260,'Viby J'),(8270,'Højbjerg'),(8300,'Odder'),(8305,'Samsø'),(8310,'Tranbjerg J'),(8320,'Mårslet'),(8330,'Beder'),(8340,'Malling'),(8350,'Hundslund'),(8355,'Solbjerg'),(8361,'Hasselager'),(8362,'Hørning'),(8370,'Hadsten'),(8380,'Trige'),(8381,'Tilst'),(8382,'Hinnerup'),(8400,'Ebeltoft'),(8410,'Rønde'),(8420,'Knebel'),(8444,'Balle'),(8450,'Hammel'),(8462,'Harlev J'),(8464,'Galten'),(8471,'Sabro'),(8472,'Sporup'),(8500,'Grenaa'),(8520,'Lystrup'),(8530,'Hjortshøj'),(8541,'Skødstrup'),(8543,'Hornslet'),(8544,'Mørke'),(8550,'Ryomgård'),(8560,'Kolind'),(8570,'Trustrup'),(8581,'Nimtofte'),(8585,'Glesborg'),(8586,'Ørum Djurs'),(8592,'Anholt'),(8600,'Silkeborg'),(8620,'Kjellerup'),(8632,'Lemming'),(8641,'Sorring'),(8643,'Ans By'),(8653,'Them'),(8654,'Bryrup'),(8660,'Skanderborg'),(8670,'Låsby'),(8680,'Ry'),(8700,'Horsens'),(8721,'Daugård'),(8722,'Hedensted'),(8723,'Løsning'),(8732,'Hovedgård'),(8740,'Brædstrup'),(8751,'Gedved'),(8752,'Østbirk'),(8762,'Flemming'),(8763,'Rask Mølle'),(8765,'Klovborg'),(8766,'Nørre Snede'),(8781,'Stenderup'),(8783,'Hornsyld'),(8789,'Endelave'),(8799,'Tunø'),(8800,'Viborg'),(8830,'Tjele'),(8831,'Løgstrup'),(8832,'Skals'),(8840,'Rødkærsbro'),(8850,'Bjerringbro'),(8860,'Ulstrup'),(8870,'Langå'),(8881,'Thorsø'),(8882,'Fårvang'),(8883,'Gjern'),(8900,'Randers C'),(8920,'Randers NV'),(8930,'Randers NØ'),(8940,'Randers SV'),(8950,'Ørsted'),(8960,'Randers SØ'),(8961,'Allingåbro'),(8963,'Auning'),(8970,'Havndal'),(8981,'Spentrup'),(8983,'Gjerlev J'),(8990,'Fårup'),(9000,'Aalborg'),(9200,'Aalborg SV'),(9210,'Aalborg SØ'),(9220,'Aalborg Øst'),(9230,'Svenstrup J'),(9240,'Nibe'),(9260,'Gistrup'),(9270,'Klarup'),(9280,'Storvorde'),(9293,'Kongerslev'),(9300,'Sæby'),(9310,'Vodskov'),(9320,'Hjallerup'),(9330,'Dronninglund'),(9340,'Asaa'),(9352,'Dybvad'),(9362,'Gandrup'),(9370,'Hals'),(9380,'Vestbjerg'),(9381,'Sulsted'),(9382,'Tylstrup'),(9400,'Nørresundby'),(9430,'Vadum'),(9440,'Aabybro'),(9460,'Brovst'),(9480,'Løkken'),(9490,'Pandrup'),(9492,'Blokhus'),(9493,'Saltum'),(9500,'Hobro'),(9510,'Arden'),(9520,'Skørping'),(9530,'Støvring'),(9541,'Suldrup'),(9550,'Mariager'),(9560,'Hadsund'),(9574,'Bælum'),(9575,'Terndrup'),(9600,'Aars'),(9610,'Nørager'),(9620,'Aalestrup'),(9631,'Gedsted'),(9632,'Møldrup'),(9640,'Farsø'),(9670,'Løgstør'),(9681,'Ranum'),(9690,'Fjerritslev'),(9700,'Brønderslev'),(9740,'Jerslev J'),(9750,'Østervrå'),(9760,'Vrå'),(9800,'Hjørring'),(9830,'Tårs'),(9850,'Hirtshals'),(9870,'Sindal'),(9881,'Bindslev'),(9900,'Frederikshavn'),(9940,'Læsø'),(9970,'Strandby'),(9981,'Jerup'),(9982,'Ålbæk'),(9990,'Skagen');
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
