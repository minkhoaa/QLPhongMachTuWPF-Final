
create  database QLPMT
go
use QLPMT
go
set dateformat DMY
	CREATE TABLE ACCOUNT ( 
	UserName NVARCHAR(100) PRIMARY KEY, 
	DisPlayName nvarchar(100) not null default 'Account',
	PassWord nvarchar(1000) not null default 'Trống',
	Type int not null default 0 
	)
	go



CREATE TABLE BENHNHAN
(
	MaBN int IDENTITY(1,1) not null,
	TenBN nvarchar(40),
	NgaySinh smalldatetime,
	DiaChi nvarchar(80),
	DienThoai varchar(20),
	GioiTinh nvarchar(20),
	TrangThai int
)
ALTER TABLE BENHNHAN ADD
CONSTRAINT BN_MaBN_PK PRIMARY KEY (MaBN)

CREATE TABLE NHANVIEN
(
	MaNV int IDENTITY(1,1)not null,
	TenNV nvarchar(40),
	NgaySinh smalldatetime, 
	DiaChi nvarchar(100),
	DienThoai varchar(20),
	GioiTinh nvarchar(20), 
	LoaiNV int,
	TrangThai int
)


ALTER TABLE NHANVIEN ADD
CONSTRAINT NV_MaNV_PK PRIMARY KEY (MaNV)

CREATE TABLE PHIEUKHAM
(
	MaPK int IDENTITY(1,1) not null,
	MaNV int,
	MaBN int,
	NgayKham smalldatetime,
	TrieuChung nvarchar(80),
	KetQua nvarchar(80),
	TrangThai int
)
ALTER TABLE PHIEUKHAM ADD
CONSTRAINT PK_MaPK_PK PRIMARY KEY (MaPK),
CONSTRAINT PK_MaNV_FK FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV),
CONSTRAINT PK_MaBN_FK FOREIGN KEY (MaBN) REFERENCES BENHNHAN(MaBN)

CREATE TABLE HOADON
(
	MaHD int IDENTITY(1,1)not null,
	MaPK int ,
	TienKham money,
	TienThuoc money,
	TongTien money,
	NgayHD smalldatetime,
	TrangThai int
)
ALTER TABLE HOADON ADD
CONSTRAINT HD_MaHD_PK PRIMARY KEY (MaHD),
CONSTRAINT HD_MaPK_FK FOREIGN KEY (MaPK) REFERENCES PHIEUKHAM(MaPK)

CREATE TABLE THUOC
(
	MaThuoc int IDENTITY(1,1)not null,
	TenThuoc nvarchar(40),
	DonViTinh nvarchar(20),
	Gia money,
	TrangThai int
)
ALTER TABLE THUOC ADD
CONSTRAINT TH_TenThuoc_PK PRIMARY KEY (MaThuoc)

CREATE TABLE CTTT
(
	MaPK int not null,
	MaThuoc int not null,
	SoLuong int,
	DonGia money,
	CachDung nvarchar(max),
	TrangThai int
)
ALTER TABLE CTTT ADD
CONSTRAINT CTTT_MaPK_TenThuoc_PK PRIMARY KEY (MaPK, MaThuoc),
CONSTRAINT CTTT_MaPK_FK FOREIGN KEY (MaPK) REFERENCES PHIEUKHAM(MaPK),
CONSTRAINT CTTT_TenThuoc_FK FOREIGN KEY (MaThuoc) REFERENCES THUOC(MaThuoc)



CREATE TABLE QUIDINH
(
	TienKham money
)
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

INSERT INTO PHIEUKHAM (MaNV, MaBN, NgayKham, TrieuChung, KetQua, TrangThai)
VALUES('1',	'1',	'01/01/2018',	N'Dị ứng theo mùa như hắt hơi, chảy nước mũi, ngứa họng hoặc ngứa, chảy nước mắt',	N'Viêm Xoan.', '1')
INSERT INTO PHIEUKHAM (MaNV, MaBN, NgayKham, TrieuChung, KetQua, TrangThai)
VALUES('3',	'5',	'01/02/2018',	N'Chảy nước mắt, chảy nước mũi, ngứa mắt/mũi, hắt hơi, phát ban và ngứa',	N'Dấu hiệu bất thường trung bình.', '1')
INSERT INTO PHIEUKHAM (MaNV, MaBN, NgayKham, TrieuChung, KetQua, TrangThai)
VALUES('1',	'1',	'07/03/2018',	N'Dị ứng theo mùa như hắt hơi, chảy nước mũi, ngứa họng hoặc ngứa, chảy nước mắt',	N'Viêm Xoan.', '1')
INSERT INTO PHIEUKHAM (MaNV, MaBN, NgayKham, TrieuChung, KetQua, TrangThai)
VALUES('2',	'3',	'15/3/2018',	N'Bỏng da', 	N'Viêm da.', '1')
INSERT INTO PHIEUKHAM (MaNV, MaBN, NgayKham, TrieuChung, KetQua, TrangThai)
VALUES('4',	'4',	'22/3/2018',	N'Đau khớp, sưng và cứng khớp,  viêm khớp',	 N'Thiếu canxi.', '1')

INSERT INTO PHIEUKHAM (MaNV, MaBN, NgayKham, TrieuChung, KetQua, TrangThai)
VALUES('1',	'8',	'25/3/2018',	N'Đau khớp',	N'Sụn khớp và thiếu canxi trầm trọng.', '1')
INSERT INTO PHIEUKHAM (MaNV, MaBN, NgayKham, TrieuChung, KetQua, TrangThai)
VALUES('3',	'9',	'04/01/2018',	N'Da nổi mẩn đỏ.',	N'Thủy đậu.', '1')

INSERT INTO PHIEUKHAM (MaNV, MaBN, NgayKham, TrieuChung, KetQua, TrangThai)
VALUES('1',	'11',	'04/04/2018',	N'Sưng và đau nhức ở cánh tay.', N'Vết thương bị nhiềm trùng.', '1')
INSERT INTO PHIEUKHAM (MaNV, MaBN, NgayKham, TrieuChung, KetQua, TrangThai)
VALUES('3',	'12',	'04/05/2018',	N'Vết thương không ngừng chảy máu',	N'Máu khó đông.', '1')
INSERT INTO PHIEUKHAM (MaNV, MaBN, NgayKham, TrieuChung, KetQua, TrangThai)
VALUES('2',	'13',	'08/05/2018',	N'Không thụ thai trong 4 năm',	N'Viêm nang buồng trứng.', '1')



INSERT INTO HOADON (MaPK, TienKham, TienThuoc, TongTien,NgayHD, TrangThai)
VALUES('1',	100000,	400000,	500000, '11/12/1970',  '1')
INSERT INTO HOADON (MaPK, TienKham, TienThuoc, TongTien,NgayHD, TrangThai)
VALUES('2',	100000,	100000,	200000, '11/12/1970',  '1')
INSERT INTO HOADON (MaPK, TienKham, TienThuoc, TongTien,NgayHD, TrangThai)
VALUES('10',	100000,	1000000,	1100000, '11/12/1970',  '1')




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


INSERT INTO CTTT (MaPK, MaThuoc, SoLuong, DonGia, CachDung, TrangThai)
VALUES 
(1, 1, 2, 20000, N'Uống sau ăn', 1),
(1, 2, 1, 15000, N'Uống trước khi ngủ', 1),
(1, 3, 1, 25000, N'Uống mỗi sáng', 1),
(2, 4, 1, 50000, N'Uống sau khi ăn', 1),
(2, 5, 1, 100000, N'Tiêm theo hướng dẫn bác sĩ', 1),
(3, 6, 2, 30000, N'Uống mỗi sáng và tối', 1),
(3, 7, 1, 18000, N'Uống trước khi ăn sáng', 1),
(4, 8, 3, 22000, N'Uống sau khi ăn', 1),
(4, 9, 1, 25000, N'Uống trước khi ăn sáng', 1),
(5, 10, 1, 26000, N'Uống buổi tối', 1),
(5, 11, 2, 20000, N'Uống sau khi ăn', 1),
(6, 12, 1, 28000, N'Uống buổi tối', 1),
(6, 13, 2, 35000, N'Uống sau bữa sáng', 1),
(7, 14, 1, 24000, N'Uống buổi tối', 1),
(7, 15, 1, 50000, N'Uống trước khi ăn', 1),
(8, 16, 1, 15000, N'Uống vào buổi tối', 1),
(8, 17, 2, 18000, N'Uống trước khi ngủ', 1),
(9, 18, 1, 12000, N'Uống vào buổi sáng', 1),
(9, 19, 1, 48000, N'Uống sau ăn', 1),
(10, 20, 1, 75000, N'Tiêm theo chỉ dẫn bác sĩ', 1);

INSERT INTO QUIDINH (TienKham)
VALUES (100000)


INSERT INTO ACCOUNT( UserName, DisPlayName, PassWord, Type) values ('admin' , 'admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 0)
INSERT INTO ACCOUNT( UserName, DisPlayName, PassWord, Type) values ('employer' , 'employer', '8efdf55724c97710333b1e6e7ad03e1b61c9c4206a35ca33898058c6e15c9c42', 1)
create table LICHHEN (
	MaLH int identity(1,1) not null, 
	TenBN nvarchar(40),
	NgaySinh smalldatetime,
	DiaChi nvarchar(80),
	DienThoai varchar(20),
	GioiTinh nvarchar(20),
	TrangThai int,
	TenNV nvarchar(40),
	NgayKham smalldatetime 
)
alter table LICHHEN add primary key(MaLH)
INSERT INTO LICHHEN (TenBN, NgaySinh, DiaChi, DienThoai, GioiTinh, TrangThai, TenNV, NgayKham)
VALUES(N'Hồ Thị Thu Thủy',	'05/01/1997', N'100 Lý Thường Kiệt, Phường 14, Quận 10, Hồ Chí Minh', '998623462', N'Nữ', '1', N'Nguyễn Minh', '11/12/1970' )

