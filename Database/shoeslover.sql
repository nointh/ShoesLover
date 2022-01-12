-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 12, 2022 at 01:56 PM
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
-- Table structure for table `admin`
--

CREATE TABLE `admin` (
  `username` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `role` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`username`, `password`, `role`) VALUES
('truongduy@gmail.com', '123456', '');

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

--
-- Dumping data for table `cart_item`
--

INSERT INTO `cart_item` (`user_id`, `product_detail_id`, `quantity`) VALUES
(1, 65, 2),
(1, 66, 2),
(1, 73, 1),
(2, 72, 5),
(4, 66, 2);

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
(39, 'Brown-Dark', 'color_Tan221846874.jpg', 1),
(40, 'Navy-White-Blk', 'color_Navy-White-Blk222123643.jpg', 1),
(41, 'Chocolate', 'color_Chocolate222619041.jpg', 1),
(42, 'Dark Olive', 'color_Dark Olive222751229.jpg', 1),
(43, 'Tan Tumbled', 'color_Tan Tumbled222953388.jpg', 1),
(44, 'Tan', 'color_Tan223302154.jpg', 1);

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
(33, 'Admin', '2022-01-11 15:51:35', 51, 'thoi ban oi', 1, 44, 30),
(34, 'Tiên', '2022-01-11 16:33:34', 51, ' Còn converse chuck taylor size 39 k vậy shop?', 1, 44, NULL),
(35, 'Duy', '2022-01-11 16:44:55', 51, 'Có đôi nike nào mắc tiền không shop?', 1, 44, NULL),
(36, 'Admin', '2022-01-11 16:02:14', 51, 'Bên shop em hết hàng sản phẩm đó rồi', 1, 44, 34),
(37, 'Huy', '2022-01-11 16:25:54', 51, 'Có bán trả góp không shop :))', 1, 44, NULL),
(38, 'An', '2022-01-11 17:33:03', 51, 'Giá quá rẻ', 0, 44, NULL),
(39, 'Hoàng', '2022-01-11 16:52:18', 51, 'shop có bán xi giày k?', 0, 44, NULL),
(40, 'Admin', '2022-01-11 17:14:05', 51, 'Bạn đã tham khảo sản phẩm nào chưa?', 1, 44, 38),
(41, 'Admin', '2022-01-11 17:14:28', 51, 'Có cần mình tư vấn gì không', 1, 44, 38),
(42, 'Tiên ', '2022-01-12 02:46:50', 53, 'còn size 39 k shop?', 1, 13, NULL),
(43, 'Admin', '2022-01-12 02:47:14', 53, 'Hế rồi bạn', 1, 13, 42);

-- --------------------------------------------------------

--
-- Table structure for table `order`
--

CREATE TABLE `order` (
  `id` int(11) NOT NULL,
  `uid` bigint(20) NOT NULL,
  `order_date` timestamp NOT NULL DEFAULT current_timestamp(),
  `address` varchar(150) COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `phone` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `total` decimal(13,2) NOT NULL,
  `status` int(11) NOT NULL DEFAULT 0,
  `reason` text COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `coupon` float NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `order`
--

INSERT INTO `order` (`id`, `uid`, `order_date`, `address`, `name`, `phone`, `total`, `status`, `reason`, `coupon`) VALUES
(23, 1, '2022-01-12 10:47:41', '163/5 xuân hoà 1, Thị xã Tân Châu, An Giang', 'Cẩm Tiên', '00485802', '1341000.00', 0, NULL, 0.1),
(25, 4, '2022-01-12 10:51:01', '163/5 xuân hoà 1, Thành phố Long Xuyên, An Giang', 'Lê An', '00485802', '1791000.00', 3, NULL, 0.1),
(26, 4, '2022-01-12 10:51:26', '163/5 xuân hoà 1, Thị xã Tân Châu, An Giang', 'Lê An', '00485802', '1341000.00', 3, NULL, 0.1);

-- --------------------------------------------------------

--
-- Table structure for table `order_detail`
--

CREATE TABLE `order_detail` (
  `order_id` int(20) NOT NULL,
  `product_detail_id` bigint(20) NOT NULL,
  `quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `order_detail`
--

INSERT INTO `order_detail` (`order_id`, `product_detail_id`, `quantity`) VALUES
(23, 72, 1),
(25, 65, 1),
(26, 72, 1);

-- --------------------------------------------------------

--
-- Table structure for table `order_payment`
--

CREATE TABLE `order_payment` (
  `order_id` int(11) NOT NULL,
  `payment_method` int(11) NOT NULL DEFAULT 1,
  `payment_status` int(11) NOT NULL DEFAULT 0,
  `payment_name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'Thanh toán khi nhận hàng'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `order_payment`
--

INSERT INTO `order_payment` (`order_id`, `payment_method`, `payment_status`, `payment_name`) VALUES
(23, 2, 1, 'Thanh toán online'),
(25, 2, 1, 'Thanh toán online'),
(26, 1, 1, 'Thanh toán khi nhận hàng');

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
(51, 44, 'product_variant_51_color_39223354799.png', 'img_big_1_51_color_39223354800.png', 'img_big_2_51_color_39223354800.png', 'img_big_3_51_color_39223354800.png', 'img_big_4_51_color_39223354800.png', 'img_big_5_51_color_39223354800.png', 'img_big_6_51_color_39223354800.png', 1),
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
(65, 51, 2, 44, 2, 1),
(66, 51, 1, 44, 4, 1),
(67, 51, 5, 44, 2, 1),
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
-- Table structure for table `rating`
--

CREATE TABLE `rating` (
  `rating_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `color_id` int(11) NOT NULL,
  `rating` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `rating`
--

INSERT INTO `rating` (`rating_id`, `product_id`, `color_id`, `rating`) VALUES
(1, 51, 44, 3),
(2, 51, 44, 5),
(3, 51, 44, 2),
(4, 51, 44, 1),
(6, 51, 44, 4),
(8, 51, 44, 4),
(9, 51, 44, 3),
(10, 52, 13, 3),
(11, 53, 13, 3),
(12, 53, 13, 4);

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
  `phone` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(80) COLLATE utf8mb4_unicode_ci NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `fullname`, `email`, `phone`, `password`, `active`) VALUES
(1, 'Cẩm Tiên', 'camtien@gmail.com', '093567233', '123', 1),
(2, 'Trường Duy', 'duy@gmail.com', '084378922', '345', 1),
(3, 'Huỳnh Long', 'long@gmail.com', '0167890345', '12', 1),
(4, 'Lê An', 'an@gmal.com', '038956278', '34', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`username`);

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
-- Indexes for table `order_payment`
--
ALTER TABLE `order_payment`
  ADD PRIMARY KEY (`order_id`);

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
-- Indexes for table `rating`
--
ALTER TABLE `rating`
  ADD PRIMARY KEY (`rating_id`);

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
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45;

--
-- AUTO_INCREMENT for table `comment`
--
ALTER TABLE `comment`
  MODIFY `comment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;

--
-- AUTO_INCREMENT for table `order`
--
ALTER TABLE `order`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT for table `order_detail`
--
ALTER TABLE `order_detail`
  MODIFY `order_id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2691713;

--
-- AUTO_INCREMENT for table `order_payment`
--
ALTER TABLE `order_payment`
  MODIFY `order_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

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
-- AUTO_INCREMENT for table `rating`
--
ALTER TABLE `rating`
  MODIFY `rating_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

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
