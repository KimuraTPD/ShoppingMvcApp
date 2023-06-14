-- MariaDB dump 10.17  Distrib 10.4.12-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: shoppingMvcApp
-- ------------------------------------------------------
-- Server version	10.4.12-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--
CREATE DATABASE IF NOT EXISTS shoppingMvcApp;
USE shoppingMvcApp;
DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20230609040541_InitialCreate','3.1.32'),('20230614065754_PurchaseHistoryCreate','3.1.32'),('20230614073236_InvestoryControlCreate','3.1.32'),('20230614073654_InvestoryControlCreate','3.1.32');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `investorycontrol`
--

DROP TABLE IF EXISTS `investorycontrol`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `investorycontrol` (
  `productId` int(11) NOT NULL AUTO_INCREMENT,
  `InvestoryAmount` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  PRIMARY KEY (`productId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `investorycontrol`
--

LOCK TABLES `investorycontrol` WRITE;
/*!40000 ALTER TABLE `investorycontrol` DISABLE KEYS */;
/*!40000 ALTER TABLE `investorycontrol` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product` (
  `productId` int(11) NOT NULL AUTO_INCREMENT,
  `productName` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `price` int(11) NOT NULL,
  `create_date` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `image_url` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `count` int(11) NOT NULL,
  PRIMARY KEY (`productId`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'帽子',1000,'2020/01/01',' /image/Product/Item001.png',50),(2,'眼鏡',5000,'2020/01/01',' /image/Product/Item002.png',50),(3,'ケース',10000,'2020/04/10',' /image/Product/Item003.png',50),(4,'カゴ',3000,'2020/04/20',' /image/Product/Item004.png',50),(5,'ボール',1000,'2020/06/07',' /image/Product/Item005.png',50),(6,'ギター',15000,'2020/06/10',' /image/Product/Item006.png',50),(7,'バッグ',4000,'2020/08/01',' /image/Product/Item007.png',50),(8,'時計',50000,'2020/08/15',' /image/Product/Item008.png',50),(9,'ハイヒール',20000,'2021/01/01',' /image/Product/Item009.png',50),(10,'靴',25000,'2021/10/20',' /image/Product/Item010.png',50),(11,'傘',600,'2022/04/05',' /image/Product/Item011.png',50),(12,'コップ',400,'2023/01/01',' /image/Product/Item013.png',50),(13,'トランプ',500,'2023/01/01',' /image/Product/Item014.png',50),(14,'本',2000,'2023/01/01',' /image/Product/Item015.png',50);
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchasehistory`
--

DROP TABLE IF EXISTS `purchasehistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchasehistory` (
  `PurchaseHistoryId` int(11) NOT NULL AUTO_INCREMENT,
  `detailsId` int(11) NOT NULL,
  `userId` int(11) NOT NULL,
  `productId` int(11) NOT NULL,
  `count` int(11) NOT NULL,
  `purchaseDate` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  PRIMARY KEY (`PurchaseHistoryId`),
  KEY `IX_PurchaseHistory_productId` (`productId`),
  KEY `IX_PurchaseHistory_userId` (`userId`),
  CONSTRAINT `FK_PurchaseHistory_Product_productId` FOREIGN KEY (`productId`) REFERENCES `product` (`productId`) ON DELETE CASCADE,
  CONSTRAINT `FK_PurchaseHistory_User_userId` FOREIGN KEY (`userId`) REFERENCES `user` (`userId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchasehistory`
--

LOCK TABLES `purchasehistory` WRITE;
/*!40000 ALTER TABLE `purchasehistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `purchasehistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `userId` int(11) NOT NULL AUTO_INCREMENT,
  `name` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `mail` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `password` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `tel` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `address` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  PRIMARY KEY (`userId`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'root','root@mail.com','root','00011112222','東京都葛飾区あああ1-2-3'),(2,'tanaka','tanaka@mail.com','1234','00011112222','大阪府大阪市いいい3-2-1'),(3,'sato','sato@gmail.com','sato1234','08012345678','兵庫県神戸市ううう10-20-30');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'shoppingMvcApp'
--

--
-- Dumping routines for database 'shoppingMvcApp'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-14 17:23:49
