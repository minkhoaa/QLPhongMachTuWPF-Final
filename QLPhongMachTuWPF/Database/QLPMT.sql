CREATE DATABASE QLPMT;
GO

USE QLPMT;
GO

SET DATEFORMAT DMY;

-- Tạo bảng ACCOUNT
CREATE TABLE ACCOUNT (
    UserName NVARCHAR(100) COLLATE SQL_Latin1_General_CP1_CS_AS PRIMARY KEY,
    DisPlayName NVARCHAR(100) NOT NULL DEFAULT 'Account',
    PassWord NVARCHAR(1000) NOT NULL DEFAULT 'Trống',
    Type INT NOT NULL DEFAULT 0
);
GO

-- Tạo bảng BENHNHAN
CREATE TABLE BENHNHAN (
    MaBN INT IDENTITY(1,1) NOT NULL,
    TenBN NVARCHAR(40),
    NgaySinh SMALLDATETIME,
    DiaChi NVARCHAR(80),
    DienThoai VARCHAR(20),
    GioiTinh NVARCHAR(20),
    TrangThai INT,
    CONSTRAINT BN_MaBN_PK PRIMARY KEY (MaBN)
);

-- Tạo bảng NHANVIEN
CREATE TABLE NHANVIEN (
    MaNV INT IDENTITY(1,1) NOT NULL,
    TenNV NVARCHAR(40),
    NgaySinh SMALLDATETIME,
    DiaChi NVARCHAR(100),
    DienThoai VARCHAR(20),
    GioiTinh NVARCHAR(20),
    LoaiNV INT,
    TrangThai INT,
    CONSTRAINT NV_MaNV_PK PRIMARY KEY (MaNV)
);

-- Tạo bảng PHIEUKHAM
CREATE TABLE PHIEUKHAM (
    MaPK INT IDENTITY(1,1) NOT NULL,
    MaNV INT,
    MaBN INT,
    NgayKham SMALLDATETIME,
    TrieuChung NVARCHAR(80),
    KetQua NVARCHAR(80),
    TrangThai INT,
    CONSTRAINT PK_MaPK_PK PRIMARY KEY (MaPK),
    CONSTRAINT PK_MaNV_FK FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV),
    CONSTRAINT PK_MaBN_FK FOREIGN KEY (MaBN) REFERENCES BENHNHAN(MaBN)
);

-- Tạo bảng HOADON
CREATE TABLE HOADON (
    MaHD INT IDENTITY(1,1) NOT NULL,
    MaPK INT,
    TienKham MONEY,
    TienThuoc MONEY,
    TongTien MONEY,
    NgayHD SMALLDATETIME,
    TrangThai INT,
    CONSTRAINT HD_MaHD_PK PRIMARY KEY (MaHD),
    CONSTRAINT HD_MaPK_FK FOREIGN KEY (MaPK) REFERENCES PHIEUKHAM(MaPK)
);

-- Tạo bảng THUOC
CREATE TABLE THUOC (
    MaThuoc INT IDENTITY(1,1) NOT NULL,
    TenThuoc NVARCHAR(40),
    DonViTinh NVARCHAR(20),
    Gia MONEY,
    TrangThai INT,
    CONSTRAINT TH_TenThuoc_PK PRIMARY KEY (MaThuoc)
);

-- Tạo bảng CTTT
CREATE TABLE CTTT (
    MaPK INT NOT NULL,
    MaThuoc INT NOT NULL,
    SoLuong INT,
    DonGia MONEY,
    CachDung NVARCHAR(MAX),
    TrangThai INT,
    CONSTRAINT CTTT_MaPK_TenThuoc_PK PRIMARY KEY (MaPK, MaThuoc),
    CONSTRAINT CTTT_MaPK_FK FOREIGN KEY (MaPK) REFERENCES PHIEUKHAM(MaPK),
    CONSTRAINT CTTT_TenThuoc_FK FOREIGN KEY (MaThuoc) REFERENCES THUOC(MaThuoc)
);

-- Tạo bảng QUIDINH
CREATE TABLE QUIDINH (
    TienKham MONEY primary key
);

-- Tạo bảng LICHHEN
CREATE TABLE LICHHEN (
    MaLH INT IDENTITY(1,1) NOT NULL,
    MaNV INT NOT NULL,
    MaBN INT NOT NULL,
    NgayHen DATE,
    GioHen TIME,
    MaPK INT UNIQUE,
	 CONSTRAINT TH_MaLH_FK PRIMARY KEY (MaLH),
    FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV),
    FOREIGN KEY (MaBN) REFERENCES BENHNHAN(MaBN),
    FOREIGN KEY (MaPK) REFERENCES PHIEUKHAM(MaPK)
);

INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Hồ Thị Thu Thủy',	'05/01/1997', N'100 Lý Thường Kiệt, Phường 14, Quận 10, Hồ Chí Minh', '998623462', N'Nữ', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Nguyễn Văn Trường', '26/4/1998',	N'212 Lý Thường Kiệt, Phường 14, Quận 10, Hồ Chí Minh', '982123312', N'Nam', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Trần Văn A',	'01/01/1976',	N'33 Lý Chính Thắng, Phường 8, Quận 3, Tp Hồ Chí Minh',	'1256387623',	N'Nam', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Võ Văn Hồng Nhật', 	'11/12/1970',	N'Tô Vĩnh Diện, Đông Hoà, Dĩ An, Bình Dương',	'1237646521',	N'Nam', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Nguyễn Thị Thương', 	'02/02/2012',	N'248 Lý Chính Thắng, Phường 9, Quận 3, Hồ Chí Minh',	'983762318',	N'Nữ', '1')	
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Nguyễn Thị Hồng Tú',	'14/1/1988',	N'87 Sư Vạn Hạnh, Phường 10, Quận 10, Hồ Chí Minh',	'1258730981',	N'Nữ', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Trần Quốc Trung',	'15/11/1998',	N'Tô Vĩnh Diện, Đông Hoà, Dĩ An, Bình Dương',	'9857821878',	N'Nam', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Nguyễn Thị Kiều Oanh',	'03/02/1996',	N'Ba Tháng Hai, Xuân Khánh, Ninh Kiều, Cần Thơ',	'1237652817',	N'Nữ', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Nguyễn Thị Ngọc Thùy',	'07/02/1998',	N'Lãnh Tú, Xuân Lãnh, Đồng Xuân, Phú Yên',	'1256384921',	N'Nữ', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Võ Thị Vân',	'15/11/1999',	N'Lãnh Trường , Xuân Lãnh, Đồng Xuân, Phú Yên',	'1256383924',	N'Nữ', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Trần Đình Sỹ',	'03/01/1990',	N'133/123C Tô Hiến Thành, Phường 13, Quận 10, Hồ Chí Minh',	'1256172111',	N'Nam', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Ngô Mỹ Anh',	'05/04/2005',	N'Phường Cầu Ông Lãnh, District 1, Ho Chi Minh City',	'9872019181',	N'Nữ', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Trần Vân',	'07/07/1995',	N'484 Lê Văn Việt, Tăng Nhơn Phú A, Quận 9, Hồ Chí Minh',	'976287165',	N'Nữ', '1')
INSERT INTO BENHNHAN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai)
VALUES(N'Nguyễn Song Nguyên',	'03/08/1992',	N'121 Nguyễn Xí, phường 26, Bình Thạnh, Hồ Chí Minh',	'1256172123',	N'Nam', '1')


INSERT INTO NHANVIEN (TenNV, NgaySinh, DiaChi, DienThoai ,GioiTinh,  LoaiNV, TrangThai)
VALUES(N'Nguyễn Minh',	'07/07/1995',	N'133/123C Tô Hiến Thành, Phường 13, Quận 10, Hồ Chí Minh' , '976287165', N'Nam' , '1', '1')
INSERT INTO NHANVIEN (TenNV, NgaySinh, DiaChi, DienThoai ,GioiTinh,  LoaiNV, TrangThai)
VALUES(N'Văn Dũng',	'07/02/1985',	N'121 Nguyễn Xí, phường 26, Bình Thạnh, Hồ Chí Minh', '976287165', N'Nam', '1', '1')
INSERT INTO NHANVIEN (TenNV, NgaySinh, DiaChi, DienThoai ,GioiTinh,  LoaiNV, TrangThai)
VALUES(N'Nguyễn Hạnh',	'07/07/1955',	N'133/123C Tô Hiến Thành, Phường 13, Quận 10, Hồ Chí Minh','976287165',N'Nữ',   '1', '1')
INSERT INTO NHANVIEN (TenNV, NgaySinh, DiaChi, DienThoai ,GioiTinh,  LoaiNV, TrangThai)
VALUES(N'Minh Khang',	'07/02/1998',	N'484 Lê Văn Việt, Tăng Nhơn Phú A, Quận 9, Hồ Chí Minh', '976287165', N'Nam' ,'1', '1')


INSERT INTO THUOC (TenThuoc, DonViTinh, Gia, TrangThai)
VALUES 
(N'Paracetamol', N'Viên', 20000, 1),
(N'Amoxicillin', N'Viên', 15000, 1),
(N'Aspirin', N'Viên', 25000, 1),
(N'Vitamin C', N'Lọ', 50000, 1),
(N'Ceftriaxone', N'Ống', 100000, 1),
(N'Ibuprofen', N'Viên', 30000, 1),
(N'Loratadine', N'Viên', 18000, 1),
(N'Prednisolone', N'Viên', 22000, 1),
(N'Omeprazole', N'Viên', 25000, 1),
(N'Diclofenac', N'Viên', 26000, 1),
(N'Metformin', N'Viên', 20000, 1),
(N'Simvastatin', N'Viên', 28000, 1),
(N'Ciprofloxacin', N'Viên', 35000, 1),
(N'Doxycycline', N'Viên', 24000, 1),
(N'Azithromycin', N'Viên', 50000, 1),
(N'Chlorpheniramine', N'Viên', 15000, 1),
(N'Ranitidine', N'Viên', 18000, 1),
(N'Albendazole', N'Viên', 12000, 1),
(N'Clarithromycin', N'Viên', 48000, 1),
(N'Ketorolac', N'Ống', 75000, 1);


INSERT INTO QUIDINH (TienKham)
VALUES (100000)


INSERT INTO ACCOUNT( UserName, DisPlayName, PassWord, Type) values ('admin' , 'admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 0)
INSERT INTO ACCOUNT( UserName, DisPlayName, PassWord, Type) values ('employer' , 'employer', '8efdf55724c97710333b1e6e7ad03e1b61c9c4206a35ca33898058c6e15c9c42', 1)

