-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 04, 2022 at 09:04 AM
-- Server version: 10.4.21-MariaDB
-- PHP Version: 8.0.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `shoeslover`
--

-- --------------------------------------------------------

--
-- Table structure for table `brand`
--

CREATE TABLE `brand` (
  `id` bigint(20) NOT NULL,
  `brand_name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `brand`
--

INSERT INTO `brand` (`id`, `brand_name`, `active`) VALUES
(1, 'Nike', 1),
(2, 'Addidas', 1),
(3, 'Puma', 1),
(4, 'Converse', 1),
(5, 'New Ballance', 1),
(6, 'Dr.Martens', 1),
(7, 'Klarna', 1),
(8, 'Khác', 1),
(9, 'Oxford', 1),
(10, 'Vans', 1),
(11, 'Filla', 1);

-- --------------------------------------------------------

--
-- Table structure for table `cart_item`
--

CREATE TABLE `cart_item` (
  `user_id` bigint(20) NOT NULL,
  `product_detail_id` bigint(20) NOT NULL,
  `quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category` (
  `id` bigint(20) NOT NULL,
  `categoryName` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`id`, `categoryName`, `active`) VALUES
(1, 'Nam', 1),
(2, 'Nữ', 1),
(3, 'Unisex', 1),
(4, 'Trẻ em', 1),
(5, 'Phụ kiện', 1),
(6, 'Sale', 1);

-- --------------------------------------------------------

--
-- Table structure for table `color`
--

CREATE TABLE `color` (
  `id` bigint(20) NOT NULL,
  `color_name` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `color_image` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `color`
--

INSERT INTO `color` (`id`, `color_name`, `color_image`, `active`) VALUES
(1, 'Đen', 'black.jpg', 1),
(2, 'Đỏ', 'red.jpg', 1),
(3, 'trắng', 'white.jpg', 1),
(4, 'Vàng', 'yellow.jpg', 1),
(5, 'Cam', 'orange.jpg', 1),
(6, 'Xám', 'gray.jpg', 1),
(7, 'blue', 'blue.jpg', 1),
(8, 'Hạt dẻ', 'chesnut.jpg', 1),
(9, 'Da báo', 'leopard.jpg', 1),
(10, 'Gold', 'gold.jpg', 1),
(11, 'Silver', 'silver.jpg', 1),
(12, 'DK nude', 'nude.jpg', 1),
(13, 'Nâu tanin', 'color_Nâu tanin210218346.jpg', 1),
(14, 'Bk/Pnk/Purp', 'BkPnkPurp.jpg', 1),
(16, 'Crm-Pea-Wht', 'color_Crm-Pea-Wht225600829.jpg', 1),
(17, 'Wht-Yellow-Org', 'color_Wht-Yellow-Org225704085.jpg', 1),
(18, 'Wht-Grn-Lav', 'color_Wht-Grn-Lav225737404.jpg', 1),
(19, 'Blk-Blu-Lim', 'color_Blk-Blu-Lim225959304.jpg', 1),
(20, 'Charcoal-Pink', 'color_Charcoal-Pink220100792.jpg', 1),
(21, 'Grey-Pink-Aqua', 'color_Grey-Pink-Aqua220142928.jpg', 1),
(22, 'Black-Pur-White', 'color_Black-Pur-White220228575.jpg', 1),
(23, 'Gry-Yellow Glow', 'color_Gry-Yellow Glow220324672.jpg', 1),
(24, 'Coral-White', 'color_Coral-White220403913.jpg', 1),
(25, 'Wht-Grn-Lav', 'color_Wht-Grn-Lav220453520.jpg', 1),
(26, 'Blk-Met-Pnk', 'color_Blk-Met-Pnk220522129.jpg', 1),
(27, 'White-Grey-Blue', 'color_White-Grey-Blue220728913.jpg', 1),
(28, 'Alum-Black-Purp', 'color_Alum-Black-Purp220824432.jpg', 1),
(29, 'Grey-Wht-Aruba', 'color_Grey-Wht-Aruba220937651.jpg', 1),
(30, 'Sand', 'color_Sand221216668.jpg', 1),
(31, 'Spiced Rum Pat', 'color_Spiced Rum Pat221324296.jpg', 1),
(32, 'Blush Smooth', 'color_Blush Smooth221354276.jpg', 1),
(33, 'Fuchsia Micro', 'color_Fuchsia Micro221452793.jpg', 1),
(34, 'Cobalt Micro', 'color_Cobalt Micro221509801.jpg', 1),
(35, 'Linen-Oat', 'color_Linen-Oat221554543.jpg', 1),
(36, 'Bomber Brown', 'color_Bomber Brown221634881.jpg', 1),
(37, 'Blue-White', 'color_Blue-White221711325.jpg', 1),
(38, 'Black-White', 'color_Black-White221751723.jpg', 1),
(39, 'Tan', 'color_Tan221846874.jpg', 1),
(40, 'Navy-White-Blk', 'color_Navy-White-Blk222123643.jpg', 1),
(41, 'Chocolate', 'color_Chocolate222619041.jpg', 1),
(42, 'Dark Olive', 'color_Dark Olive222751229.jpg', 1),
(43, 'Tan Tumbled', 'color_Tan Tumbled222953388.jpg', 1);

-- --------------------------------------------------------

--
-- Table structure for table `comment`
--

CREATE TABLE `comment` (
  `comment_id` int(11) NOT NULL,
  `comment_name` varchar(250) COLLATE utf8mb4_unicode_ci NOT NULL,
  `comment_date` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `comment_product_id` int(11) NOT NULL,
  `comment_text` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `comment_status` int(11) NOT NULL,
  `comment_color_id` int(11) NOT NULL,
  `comment_parent_comment` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `comment`
--

INSERT INTO `comment` (`comment_id`, `comment_name`, `comment_date`, `comment_product_id`, `comment_text`, `comment_status`, `comment_color_id`, `comment_parent_comment`) VALUES
(1, 'Trường Duy', '2022-01-03 16:14:34', 51, 'Sản phẩm đẹp quá shop ơi!!!', 1, 1, NULL),
(2, 'Cẩm Tiên', '2022-01-03 07:45:30', 52, 'Sản phẩm quá hợp với túi tiền!!!', 0, 0, NULL),
(3, 'Quốc An', '2022-01-03 15:11:47', 51, 'Sản phẩm quá chất!!', 1, 39, NULL),
(4, 'T', '2022-01-03 16:16:48', 51, 'Đi chơi mang khá là thích!!', 1, 1, NULL),
(5, 'Cẩm Tiên', '2022-01-03 16:17:53', 51, 'Hay lắm bro!!', 1, 1, NULL),
(7, 'Duy', '2022-01-03 15:11:43', 51, '\n             Quá rẻ       ', 1, 1, NULL),
(9, 'Duy', '2022-01-03 16:18:02', 51, '\n               Buồn     ', 1, 39, NULL),
(10, 'Huy', '2022-01-03 15:11:39', 51, '\n             Giá Quá hời       ', 1, 1, NULL),
(11, 'Tuấn', '2022-01-03 16:18:32', 51, '\n            Hàng quá xịn        ', 1, 1, NULL),
(12, 'Hoàng', '2022-01-03 16:24:12', 51, '\n                    Xịn quá', 0, 39, NULL),
(13, 'Tiên', '2022-01-03 16:22:44', 51, '\n                    Giày xấu', 0, 39, NULL),
(14, 'Tiên', '2022-01-03 16:12:53', 51, '\n                    Giày đẹp quá anh ơi', 1, 39, NULL),
(15, 'Trâm', '2022-01-03 16:26:25', 51, ' Giày quá xấu và mắc !! không đáng mua', 1, 1, NULL),
(17, 'Admin', '2022-01-03 17:21:43', 51, 'ok bạn', 1, 1, 15),
(18, 'Quỳnh', '2022-01-03 17:31:26', 51, ' Giày quá ok', 1, 39, NULL),
(22, 'DuyAmin', '2022-01-03 18:01:32', 51, 'ok', 1, 39, 9),
(23, 'DuyAmin', '2022-01-03 18:04:14', 51, 'ok', 1, 1, 5),
(24, 'DuyAmin', '2022-01-03 18:16:41', 51, 'Bạn muốn lấy thêm không', 1, 1, 5);

-- --------------------------------------------------------

--
-- Table structure for table `order`
--

CREATE TABLE `order` (
  `id` bigint(20) NOT NULL,
  `uid` bigint(20) NOT NULL,
  `order_date` date NOT NULL,
  `address` varchar(150) COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `phone` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `total` decimal(13,2) NOT NULL,
  `status` smallint(6) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `order`
--

INSERT INTO `order` (`id`, `uid`, `order_date`, `address`, `name`, `phone`, `total`, `status`) VALUES
(1, 1, '2021-12-02', '', '', '', '500.00', 1),
(2, 2, '2021-12-08', '', '', '', '1000.00', 0),
(3, 3, '2021-12-01', '', '', '', '400.00', 0),
(4, 4, '2021-12-02', '', '', '', '1000.00', 1),
(5, 1, '2021-12-01', '', '', '', '500.00', 0),
(6, 2, '2021-12-01', '', '', '', '1000.00', 0),
(7, 2, '2021-12-01', '', '', '', '700000.00', 0),
(8, 2, '2022-01-04', '163/3 Xuân Hoà 1, Thành phố Châu Đốc, Bạc Liêu', 'Trường Duy', '0845437562', '1990000.00', 0);

-- --------------------------------------------------------

--
-- Table structure for table `order_detail`
--

CREATE TABLE `order_detail` (
  `order_id` bigint(20) NOT NULL,
  `product_detail_id` bigint(20) NOT NULL,
  `quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `order_detail`
--

INSERT INTO `order_detail` (`order_id`, `product_detail_id`, `quantity`) VALUES
(8, 66, 1);

-- --------------------------------------------------------

--
-- Table structure for table `product`
--

CREATE TABLE `product` (
  `id` bigint(20) NOT NULL,
  `productName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `subcategory_id` bigint(255) NOT NULL,
  `brand_id` bigint(20) NOT NULL,
  `gender` tinyint(4) NOT NULL,
  `default_image` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `regular_price` decimal(13,2) NOT NULL,
  `description` varchar(500) COLLATE utf8mb4_unicode_ci NOT NULL,
  `sale_price` decimal(13,2) NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT 1,
  `product_tag` varchar(250) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `product`
--

INSERT INTO `product` (`id`, `productName`, `subcategory_id`, `brand_id`, `gender`, `default_image`, `regular_price`, `description`, `sale_price`, `active`, `product_tag`) VALUES
(50, 'Men\'s Madden Alk Dress Shoes', 4, 9, 1, 'product_default_img_50223805881.png', '3500000.00', 'Giày được làm thủ công, đến từ thương hiệu nổi tiếng Anh quốc', '3190000.00', 1, 'SP011'),
(51, 'Men\'s Perry Ellis Squire Chelsea Dress Boots', 2, 9, 1, 'product_default_img_0223312225.png', '2400000.00', 'Giày làm bằng da', '1990000.00', 1, 'SP015'),
(52, 'Men\'s Freeman Wyatt Chukka Dress Boots', 2, 6, 1, 'product_default_img_52224032624.png', '1800000.00', 'Giày da', '1490000.00', 1, 'SP019'),
(53, 'Men\'s Clarks Jaxen Chelsea Boots', 2, 9, 1, 'product_default_img_0224154465.png', '2400000.00', 'Giày da', '2000000.00', 1, 'SP088');

-- --------------------------------------------------------

--
-- Table structure for table `product_color_variant`
--

CREATE TABLE `product_color_variant` (
  `product_id` bigint(20) NOT NULL,
  `color_id` bigint(20) NOT NULL,
  `product_variant_image` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `img_big_1` varchar(250) COLLATE utf8mb4_unicode_ci NOT NULL,
  `img_big_2` varchar(250) COLLATE utf8mb4_unicode_ci NOT NULL,
  `img_big_3` varchar(250) COLLATE utf8mb4_unicode_ci NOT NULL,
  `img_big_4` varchar(250) COLLATE utf8mb4_unicode_ci NOT NULL,
  `img_big_5` varchar(250) COLLATE utf8mb4_unicode_ci NOT NULL,
  `img_big_6` varchar(250) COLLATE utf8mb4_unicode_ci NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `product_color_variant`
--

INSERT INTO `product_color_variant` (`product_id`, `color_id`, `product_variant_image`, `img_big_1`, `img_big_2`, `img_big_3`, `img_big_4`, `img_big_5`, `img_big_6`, `active`) VALUES
(50, 1, 'product_variant_50_color_1220512613.png', 'img_big_1_50_color_1220512613.png', 'img_big_2_50_color_1220512614.png', 'img_big_3_50_color_1220512614.png', 'img_big_4_50_color_1220512614.png', 'img_big_5_50_color_1220512614.png', 'img_big_6_50_color_1220512614.png', 1),
(51, 1, 'product_variant_51_color_1223549697.png', 'img_big_1_51_color_1223549697.png', 'img_big_2_51_color_1223549697.png', 'img_big_3_51_color_1223549697.png', 'img_big_4_51_color_1223549697.png', 'img_big_5_51_color_1223549697.png', 'img_big_6_51_color_1223549697.png', 1),
(51, 39, 'product_variant_51_color_39223354799.png', 'img_big_1_51_color_39223354800.png', 'img_big_2_51_color_39223354800.png', 'img_big_3_51_color_39223354800.png', 'img_big_4_51_color_39223354800.png', 'img_big_5_51_color_39223354800.png', 'img_big_6_51_color_39223354800.png', 1),
(52, 13, 'product_variant_52_color_13223947351.png', 'img_big_1_52_color_13223947351.png', 'img_big_2_52_color_13223947351.png', 'img_big_3_52_color_13223947351.png', 'img_big_4_52_color_13223947351.png', 'img_big_5_52_color_13223947351.png', 'img_big_6_52_color_13223947351.png', 1),
(53, 6, 'product_variant_53_color_6224546342.png', 'img_big_1_53_color_6224546342.png', 'img_big_2_53_color_6224546342.png', 'img_big_3_53_color_6224546342.png', 'img_big_4_53_color_6224546342.png', 'img_big_5_53_color_6224546342.png', 'img_big_6_53_color_6224546342.png', 1),
(53, 13, 'product_variant_53_color_13224241737.png', 'img_big_1_53_color_13224241737.png', 'img_big_2_53_color_13224241737.png', 'img_big_3_53_color_13224241737.png', 'img_big_4_53_color_13224241737.png', 'img_big_5_53_color_13224241737.png', 'img_big_6_53_color_13224241737.png', 1);

-- --------------------------------------------------------

--
-- Table structure for table `product_detail`
--

CREATE TABLE `product_detail` (
  `id` bigint(20) NOT NULL,
  `product_id` bigint(20) NOT NULL,
  `size_id` bigint(20) NOT NULL,
  `color_id` bigint(20) NOT NULL,
  `quantity` int(11) NOT NULL DEFAULT 0,
  `active` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `product_detail`
--

INSERT INTO `product_detail` (`id`, `product_id`, `size_id`, `color_id`, `quantity`, `active`) VALUES
(62, 50, 2, 1, 7, 1),
(63, 50, 3, 1, 1, 1),
(64, 50, 4, 1, 2, 1),
(65, 51, 2, 39, 2, 1),
(66, 51, 1, 39, 4, 1),
(67, 51, 5, 39, 2, 1),
(68, 51, 3, 1, 5, 1),
(69, 51, 2, 1, 3, 1),
(70, 52, 1, 13, 3, 1),
(71, 52, 6, 13, 2, 1),
(72, 52, 3, 13, 5, 1),
(73, 53, 1, 13, 2, 1),
(74, 53, 2, 13, 1, 1),
(75, 53, 5, 13, 4, 1),
(76, 53, 4, 6, 3, 1),
(77, 53, 1, 6, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `size`
--

CREATE TABLE `size` (
  `id` bigint(20) NOT NULL,
  `size_name` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `size`
--

INSERT INTO `size` (`id`, `size_name`, `active`) VALUES
(1, '38', 1),
(2, '39', 1),
(3, '40', 1),
(4, '41', 1),
(5, '42', 1),
(6, '43', 1);

-- --------------------------------------------------------

--
-- Table structure for table `subcategory`
--

CREATE TABLE `subcategory` (
  `id` bigint(20) NOT NULL,
  `category_id` bigint(20) NOT NULL,
  `subcategory_name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `subcategory`
--

INSERT INTO `subcategory` (`id`, `category_id`, `subcategory_name`, `active`) VALUES
(1, 1, 'Giày sneaker', 1),
(2, 1, 'Giày cao cổ', 1),
(3, 1, 'Giày xăng đan', 1),
(4, 1, 'Giày da', 1),
(5, 1, 'Giày lười', 1),
(6, 2, 'Giày xăng đan', 1),
(7, 2, 'Giày lười', 1),
(8, 2, 'Giày cao gót', 1),
(9, 2, 'Giày cao cổ', 1),
(10, 2, 'Giày sneaker', 1),
(11, 3, 'Giày thể thao', 1),
(12, 3, 'Giày sneaker', 1),
(13, 3, 'Giày da', 1),
(14, 3, 'Giày cao cổ', 1),
(15, 3, 'Giày xăng đan', 1),
(16, 3, 'Giày lười', 1),
(17, 4, 'Giày bé trai', 1),
(18, 4, 'Giày bé gái', 1),
(19, 5, 'Vớ', 1),
(20, 5, 'Xi đánh giày', 1),
(21, 5, 'Dây giày', 1),
(22, 5, 'Lót giày', 1),
(23, 5, 'Bàn chải đáng giày', 1),
(24, 6, 'Nam', 1),
(25, 6, 'Nữ', 1);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` bigint(20) NOT NULL,
  `fullname` varchar(80) COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(80) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(80) COLLATE utf8mb4_unicode_ci NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `fullname`, `email`, `password`, `active`) VALUES
(1, 'Cẩm Tiên', 'camtien@gmail.com', '123', 1),
(2, 'Trường Duy', 'duy@gmail.com', '345', 1),
(3, 'Huỳnh Long', 'long@gmail.com', '12', 1),
(4, 'Lê An', 'an@gmal.com', '34', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `brand`
--
ALTER TABLE `brand`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `cart_item`
--
ALTER TABLE `cart_item`
  ADD PRIMARY KEY (`user_id`,`product_detail_id`);

--
-- Indexes for table `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `color`
--
ALTER TABLE `color`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `comment`
--
ALTER TABLE `comment`
  ADD PRIMARY KEY (`comment_id`),
  ADD KEY `comment_product_id` (`comment_product_id`),
  ADD KEY `comment_color_id` (`comment_color_id`);

--
-- Indexes for table `order`
--
ALTER TABLE `order`
  ADD PRIMARY KEY (`id`),
  ADD KEY `order_fk0` (`uid`);

--
-- Indexes for table `order_detail`
--
ALTER TABLE `order_detail`
  ADD PRIMARY KEY (`order_id`,`product_detail_id`);

--
-- Indexes for table `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`id`),
  ADD KEY `product_fk0` (`subcategory_id`),
  ADD KEY `product_fk1` (`brand_id`);

--
-- Indexes for table `product_color_variant`
--
ALTER TABLE `product_color_variant`
  ADD PRIMARY KEY (`product_id`,`color_id`),
  ADD KEY `product_color_variant_fk1` (`color_id`);

--
-- Indexes for table `product_detail`
--
ALTER TABLE `product_detail`
  ADD PRIMARY KEY (`id`),
  ADD KEY `product_detail_fk0` (`product_id`),
  ADD KEY `product_detail_fk1` (`size_id`),
  ADD KEY `product_detail_fk2` (`color_id`);

--
-- Indexes for table `size`
--
ALTER TABLE `size`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `subcategory`
--
ALTER TABLE `subcategory`
  ADD PRIMARY KEY (`id`),
  ADD KEY `subcategory_fk0` (`category_id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `brand`
--
ALTER TABLE `brand`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `category`
--
ALTER TABLE `category`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `color`
--
ALTER TABLE `color`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;

--
-- AUTO_INCREMENT for table `comment`
--
ALTER TABLE `comment`
  MODIFY `comment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `order`
--
ALTER TABLE `order`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `product`
--
ALTER TABLE `product`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=54;

--
-- AUTO_INCREMENT for table `product_detail`
--
ALTER TABLE `product_detail`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=78;

--
-- AUTO_INCREMENT for table `size`
--
ALTER TABLE `size`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `subcategory`
--
ALTER TABLE `subcategory`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `order`
--
ALTER TABLE `order`
  ADD CONSTRAINT `order_fk0` FOREIGN KEY (`uid`) REFERENCES `user` (`id`);

--
-- Constraints for table `product`
--
ALTER TABLE `product`
  ADD CONSTRAINT `product_fk0` FOREIGN KEY (`subcategory_id`) REFERENCES `subcategory` (`id`),
  ADD CONSTRAINT `product_fk1` FOREIGN KEY (`brand_id`) REFERENCES `brand` (`id`);

--
-- Constraints for table `product_color_variant`
--
ALTER TABLE `product_color_variant`
  ADD CONSTRAINT `product_color_variant_fk0` FOREIGN KEY (`product_id`) REFERENCES `product` (`id`),
  ADD CONSTRAINT `product_color_variant_fk1` FOREIGN KEY (`color_id`) REFERENCES `color` (`id`);

--
-- Constraints for table `product_detail`
--
ALTER TABLE `product_detail`
  ADD CONSTRAINT `product_detail_fk0` FOREIGN KEY (`product_id`) REFERENCES `product` (`id`),
  ADD CONSTRAINT `product_detail_fk1` FOREIGN KEY (`size_id`) REFERENCES `size` (`id`),
  ADD CONSTRAINT `product_detail_fk2` FOREIGN KEY (`color_id`) REFERENCES `color` (`id`);

--
-- Constraints for table `subcategory`
--
ALTER TABLE `subcategory`
  ADD CONSTRAINT `subcategory_fk0` FOREIGN KEY (`category_id`) REFERENCES `category` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
