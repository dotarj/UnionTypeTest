CREATE TABLE IF NOT EXISTS `Tenants` (
    `Id` int(11) NOT NULL AUTO_INCREMENT,
    `Name` varchar(50) NOT NULL,
    KEY `Index 1` (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

INSERT INTO `Tenants` (`Id`, `Name`) VALUES
     (1, 'Tenant A'),
     (2, 'Tenant B');

CREATE TABLE IF NOT EXISTS `FilesAndFolders` (
    `Id` int(11) NOT NULL AUTO_INCREMENT,
    `TenantId` int(11) DEFAULT NULL,
    `FolderId` int(11) NOT NULL DEFAULT 0,
    `Name` varchar(50) NOT NULL DEFAULT '0',
    `Size` int(11) DEFAULT 0,
    `Description` varchar(50) DEFAULT NULL,
    `Type` varchar(50) NOT NULL,
    KEY `Id` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

INSERT INTO `FilesAndFolders` (`Id`, `TenantId`, `FolderId`, `Name`, `Size`, `Description`, `Type`) VALUES
    (1, 1, 0, 'aaa', 123, NULL, 'FILE'),
    (2, 1, 0, 'bbb', 0, 'asdsadad', 'FOLDER'),
    (3, 1, 0, 'ccc', 123, NULL, 'FILE'),
    (4, 2, 0, 'ddd', 321, NULL, 'FILE');
