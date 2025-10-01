BEGIN;

-- Insert Carts
INSERT INTO carts (cart_id, member_id, created_at, status) VALUES
-- Active Carts
('f47ac10b-58cc-4372-a567-0e02b2c3d479', '550e8400-e29b-41d4-a716-446655440000', '2024-01-15 10:30:00+00', 'Active'),
('6ba7b810-9dad-11d1-80b4-00c04fd430c8', '6ba7b811-9dad-11d1-80b4-00c04fd430c8', '2024-01-18 14:45:00+00', 'Active'),
('7d793037-a076-4a9e-9a3b-5f6e8f0a6d4b', '7d793038-a076-4a9e-9a3b-5f6e8f0a6d4b', '2024-01-20 09:15:00+00', 'Active'),
('8e296a06-7a2a-4c7c-8d7e-9f8a0b1c2d3e', '8e296a07-7a2a-4c7c-8d7e-9f8a0b1c2d3e', '2024-01-22 16:20:00+00', 'Active'),
('9f496b08-8b3b-4d8d-9e8f-af9b0c1d3e4f', '9f496b09-8b3b-4d8d-9e8f-af9b0c1d3e4f', '2024-01-25 11:50:00+00', 'Active'),
-- Checked Out Carts
('a0b56c0a-9c4c-4e9e-af90-bf0c1d2e4f50', 'a0b56c0b-9c4c-4e9e-af90-bf0c1d2e4f50', '2023-12-10 13:30:00+00', 'CheckedOut'),
('b1c67d0c-ad5d-4f9f-b0a1-cf1d2e3f5061', 'b1c67d0d-ad5d-4f9f-b0a1-cf1d2e3f5061', '2023-12-15 15:45:00+00', 'CheckedOut'),
('c2d78e0e-be6e-5090-c1b2-d01e3f4f6072', 'c2d78e0f-be6e-5090-c1b2-d01e3f4f6072', '2023-12-20 10:20:00+00', 'CheckedOut'),
('d3e89f10-cf7f-5191-d2c3-e12f4f506083', 'd3e89f11-cf7f-5191-d2c3-e12f4f506083', '2023-12-28 17:10:00+00', 'CheckedOut'),
('e4f9a012-d080-5292-e3d4-f23450607094', 'e4f9a013-d080-5292-e3d4-f23450607094', '2024-01-05 12:25:00+00', 'CheckedOut');

-- Insert Cart Items
INSERT INTO cart_items (cart_item_id, cart_id, book_id, quantity, added_at) VALUES
-- Items in Cart 1 (User 1 - Active)
('f47ac10c-58cc-4372-a567-0e02b2c3d47a', 'f47ac10b-58cc-4372-a567-0e02b2c3d479', '550e8401-e29b-41d4-a716-446655440001', 2, '2024-01-15 10:35:00+00'),
('f47ac10d-58cc-4372-a567-0e02b2c3d47b', 'f47ac10b-58cc-4372-a567-0e02b2c3d479', '6ba7b812-9dad-11d1-80b4-00c04fd430c9', 1, '2024-01-15 10:40:00+00'),
('f47ac10e-58cc-4372-a567-0e02b2c3d47c', 'f47ac10b-58cc-4372-a567-0e02b2c3d479', '7d793039-a076-4a9e-9a3b-5f6e8f0a6d4c', 3, '2024-01-16 14:20:00+00'),
-- Items in Cart 2 (User 2 - Active)
('6ba7b813-9dad-11d1-80b4-00c04fd430ca', '6ba7b810-9dad-11d1-80b4-00c04fd430c8', '8e296a08-7a2a-4c7c-8d7e-9f8a0b1c2d3f', 1, '2024-01-18 14:50:00+00'),
('6ba7b814-9dad-11d1-80b4-00c04fd430cb', '6ba7b810-9dad-11d1-80b4-00c04fd430c8', '9f496b0a-8b3b-4d8d-9e8f-af9b0c1d3e50', 2, '2024-01-19 09:30:00+00'),
-- Items in Cart 3 (User 3 - Active)
('7d79303a-a076-4a9e-9a3b-5f6e8f0a6d4d', '7d793037-a076-4a9e-9a3b-5f6e8f0a6d4b', 'a0b56c0c-9c4c-4e9e-af90-bf0c1d2e4f51', 1, '2024-01-20 09:20:00+00'),
('7d79303b-a076-4a9e-9a3b-5f6e8f0a6d4e', '7d793037-a076-4a9e-9a3b-5f6e8f0a6d4b', 'b1c67d0e-ad5d-4f9f-b0a1-cf1d2e3f5062', 1, '2024-01-20 11:45:00+00'),
('7d79303c-a076-4a9e-9a3b-5f6e8f0a6d4f', '7d793037-a076-4a9e-9a3b-5f6e8f0a6d4b', 'c2d78e10-be6e-5090-c1b2-d01e3f4f6073', 4, '2024-01-21 16:10:00+00'),
-- Items in Cart 4 (User 4 - Active)
('8e296a09-7a2a-4c7c-8d7e-9f8a0b1c2d40', '8e296a06-7a2a-4c7c-8d7e-9f8a0b1c2d3e', 'd3e89f12-cf7f-5191-d2c3-e12f4f506084', 1, '2024-01-22 16:25:00+00'),
('8e296a0a-7a2a-4c7c-8d7e-9f8a0b1c2d41', '8e296a06-7a2a-4c7c-8d7e-9f8a0b1c2d3e', '550e8401-e29b-41d4-a716-446655440001', 2, '2024-01-23 10:15:00+00'),
-- Items in Cart 5 (User 5 - Active)
('9f496b0b-8b3b-4d8d-9e8f-af9b0c1d3e51', '9f496b08-8b3b-4d8d-9e8f-af9b0c1d3e4f', '6ba7b812-9dad-11d1-80b4-00c04fd430c9', 5, '2024-01-25 11:55:00+00'),
-- Items in Cart 6 (User 6 - CheckedOut)
('a0b56c0d-9c4c-4e9e-af90-bf0c1d2e4f52', 'a0b56c0a-9c4c-4e9e-af90-bf0c1d2e4f50', '7d793039-a076-4a9e-9a3b-5f6e8f0a6d4c', 2, '2023-12-10 13:35:00+00'),
('a0b56c0e-9c4c-4e9e-af90-bf0c1d2e4f53', 'a0b56c0a-9c4c-4e9e-af90-bf0c1d2e4f50', '8e296a08-7a2a-4c7c-8d7e-9f8a0b1c2d3f', 1, '2023-12-10 13:40:00+00'),
-- Items in Cart 7 (User 7 - CheckedOut)
('b1c67d0f-ad5d-4f9f-b0a1-cf1d2e3f5063', 'b1c67d0c-ad5d-4f9f-b0a1-cf1d2e3f5061', '9f496b0a-8b3b-4d8d-9e8f-af9b0c1d3e50', 3, '2023-12-15 15:50:00+00'),
('b1c67d10-ad5d-4f9f-b0a1-cf1d2e3f5064', 'b1c67d0c-ad5d-4f9f-b0a1-cf1d2e3f5061', 'a0b56c0c-9c4c-4e9e-af90-bf0c1d2e4f51', 1, '2023-12-15 16:00:00+00'),
-- Items in Cart 8 (User 8 - CheckedOut)
('c2d78e11-be6e-5090-c1b2-d01e3f4f6074', 'c2d78e0e-be6e-5090-c1b2-d01e3f4f6072', 'b1c67d0e-ad5d-4f9f-b0a1-cf1d2e3f5062', 1, '2023-12-20 10:25:00+00'),
-- Items in Cart 9 (User 9 - CheckedOut)
('d3e89f13-cf7f-5191-d2c3-e12f4f506085', 'd3e89f10-cf7f-5191-d2c3-e12f4f506083', 'c2d78e10-be6e-5090-c1b2-d01e3f4f6073', 2, '2023-12-28 17:15:00+00'),
('d3e89f14-cf7f-5191-d2c3-e12f4f506086', 'd3e89f10-cf7f-5191-d2c3-e12f4f506083', 'd3e89f12-cf7f-5191-d2c3-e12f4f506084', 1, '2023-12-28 17:20:00+00'),
-- Items in Cart 10 (User 10 - CheckedOut)
('e4f9a014-d080-5292-e3d4-f23450607095', 'e4f9a012-d080-5292-e3d4-f23450607094', '550e8401-e29b-41d4-a716-446655440001', 1, '2024-01-05 12:30:00+00'),
('e4f9a015-d080-5292-e3d4-f23450607096', 'e4f9a012-d080-5292-e3d4-f23450607094', '6ba7b812-9dad-11d1-80b4-00c04fd430c9', 2, '2024-01-05 12:35:00+00');

-- Insert Wishlists
INSERT INTO wishlists (wishlist_id, member_id, name, created_at) VALUES
                                                                     ('f47ac10f-58cc-4372-a567-0e02b2c3d47d', '550e8400-e29b-41d4-a716-446655440000', 'Must Read', '2023-11-01 10:00:00+00'),
                                                                     ('6ba7b815-9dad-11d1-80b4-00c04fd430cc', '550e8400-e29b-41d4-a716-446655440000', 'Summer Reading', '2023-12-15 14:30:00+00'),
                                                                     ('7d79303d-a076-4a9e-9a3b-5f6e8f0a6d50', '6ba7b811-9dad-11d1-80b4-00c04fd430c8', 'Favorites', '2023-10-20 09:15:00+00'),
                                                                     ('8e296a0b-7a2a-4c7c-8d7e-9f8a0b1c2d42', '7d793038-a076-4a9e-9a3b-5f6e8f0a6d4b', 'Gift Ideas', '2023-11-10 16:45:00+00'),
                                                                     ('9f496b0c-8b3b-4d8d-9e8f-af9b0c1d3e52', '8e296a07-7a2a-4c7c-8d7e-9f8a0b1c2d3e', 'To Buy Later', '2023-12-01 11:20:00+00'),
                                                                     ('a0b56c0f-9c4c-4e9e-af90-bf0c1d2e4f54', '9f496b09-8b3b-4d8d-9e8f-af9b0c1d3e4f', 'Classics Collection', '2023-09-15 13:00:00+00'),
                                                                     ('b1c67d11-ad5d-4f9f-b0a1-cf1d2e3f5065', 'a0b56c0b-9c4c-4e9e-af90-bf0c1d2e4f50', 'Mystery Novels', '2023-11-25 15:30:00+00'),
                                                                     ('c2d78e12-be6e-5090-c1b2-d01e3f4f6075', 'b1c67d0d-ad5d-4f9f-b0a1-cf1d2e3f5061', 'Science Fiction', '2023-10-05 12:45:00+00'),
                                                                     ('d3e89f15-cf7f-5191-d2c3-e12f4f506087', 'c2d78e0f-be6e-5090-c1b2-d01e3f4f6072', 'Biography Corner', '2023-12-20 10:10:00+00'),
                                                                     ('e4f9a016-d080-5292-e3d4-f23450607097', 'd3e89f11-cf7f-5191-d2c3-e12f4f506083', 'Book Club Picks', '2024-01-03 14:20:00+00'),
                                                                     ('f5f9a017-d181-5393-f4e5-023560807098', 'e4f9a013-d080-5292-e3d4-f23450607094', 'Professional Dev', '2023-11-18 09:30:00+00');

-- Insert Wishlist Items
INSERT INTO wishlist_items (wishlist_item_id, wishlist_id, book_id, added_at) VALUES
-- Items in Wishlist 1 (User 1 - Must Read)
('f47ac110-58cc-4372-a567-0e02b2c3d47e', 'f47ac10f-58cc-4372-a567-0e02b2c3d47d', '8e296a08-7a2a-4c7c-8d7e-9f8a0b1c2d3f', '2023-11-01 10:15:00+00'),
('f47ac111-58cc-4372-a567-0e02b2c3d47f', 'f47ac10f-58cc-4372-a567-0e02b2c3d47d', '9f496b0a-8b3b-4d8d-9e8f-af9b0c1d3e50', '2023-11-05 14:20:00+00'),
('f47ac112-58cc-4372-a567-0e02b2c3d480', 'f47ac10f-58cc-4372-a567-0e02b2c3d47d', 'a0b56c0c-9c4c-4e9e-af90-bf0c1d2e4f51', '2023-11-12 16:30:00+00'),
('f47ac113-58cc-4372-a567-0e02b2c3d481', 'f47ac10f-58cc-4372-a567-0e02b2c3d47d', 'b1c67d0e-ad5d-4f9f-b0a1-cf1d2e3f5062', '2023-11-20 11:45:00+00'),
-- Items in Wishlist 2 (User 1 - Summer Reading)
('6ba7b816-9dad-11d1-80b4-00c04fd430cd', '6ba7b815-9dad-11d1-80b4-00c04fd430cc', 'c2d78e10-be6e-5090-c1b2-d01e3f4f6073', '2023-12-15 14:35:00+00'),
('6ba7b817-9dad-11d1-80b4-00c04fd430ce', '6ba7b815-9dad-11d1-80b4-00c04fd430cc', 'd3e89f12-cf7f-5191-d2c3-e12f4f506084', '2023-12-18 10:20:00+00'),
-- Items in Wishlist 3 (User 2 - Favorites)
('7d79303e-a076-4a9e-9a3b-5f6e8f0a6d51', '7d79303d-a076-4a9e-9a3b-5f6e8f0a6d50', '550e8401-e29b-41d4-a716-446655440001', '2023-10-20 09:30:00+00'),
('7d79303f-a076-4a9e-9a3b-5f6e8f0a6d52', '7d79303d-a076-4a9e-9a3b-5f6e8f0a6d50', '6ba7b812-9dad-11d1-80b4-00c04fd430c9', '2023-10-25 15:45:00+00'),
('7d793040-a076-4a9e-9a3b-5f6e8f0a6d53', '7d79303d-a076-4a9e-9a3b-5f6e8f0a6d50', '7d793039-a076-4a9e-9a3b-5f6e8f0a6d4c', '2023-11-02 13:10:00+00'),
-- Items in Wishlist 4 (User 3 - Gift Ideas)
('8e296a0c-7a2a-4c7c-8d7e-9f8a0b1c2d43', '8e296a0b-7a2a-4c7c-8d7e-9f8a0b1c2d42', '8e296a08-7a2a-4c7c-8d7e-9f8a0b1c2d3f', '2023-11-10 17:00:00+00'),
('8e296a0d-7a2a-4c7c-8d7e-9f8a0b1c2d44', '8e296a0b-7a2a-4c7c-8d7e-9f8a0b1c2d42', '9f496b0a-8b3b-4d8d-9e8f-af9b0c1d3e50', '2023-11-15 12:30:00+00'),
('8e296a0e-7a2a-4c7c-8d7e-9f8a0b1c2d45', '8e296a0b-7a2a-4c7c-8d7e-9f8a0b1c2d42', 'a0b56c0c-9c4c-4e9e-af90-bf0c1d2e4f51', '2023-11-22 14:50:00+00'),
-- Items in Wishlist 5 (User 4 - To Buy Later)
('9f496b0d-8b3b-4d8d-9e8f-af9b0c1d3e53', '9f496b0c-8b3b-4d8d-9e8f-af9b0c1d3e52', 'b1c67d0e-ad5d-4f9f-b0a1-cf1d2e3f5062', '2023-12-01 11:30:00+00'),
('9f496b0e-8b3b-4d8d-9e8f-af9b0c1d3e54', '9f496b0c-8b3b-4d8d-9e8f-af9b0c1d3e52', 'c2d78e10-be6e-5090-c1b2-d01e3f4f6073', '2023-12-10 16:15:00+00'),
-- Items in Wishlist 6 (User 5 - Classics Collection)
('a0b56c10-9c4c-4e9e-af90-bf0c1d2e4f55', 'a0b56c0f-9c4c-4e9e-af90-bf0c1d2e4f54', '550e8401-e29b-41d4-a716-446655440001', '2023-09-15 13:15:00+00'),
('a0b56c11-9c4c-4e9e-af90-bf0c1d2e4f56', 'a0b56c0f-9c4c-4e9e-af90-bf0c1d2e4f54', '6ba7b812-9dad-11d1-80b4-00c04fd430c9', '2023-09-20 10:40:00+00'),
('a0b56c12-9c4c-4e9e-af90-bf0c1d2e4f57', 'a0b56c0f-9c4c-4e9e-af90-bf0c1d2e4f54', '7d793039-a076-4a9e-9a3b-5f6e8f0a6d4c', '2023-10-01 14:25:00+00'),
('a0b56c13-9c4c-4e9e-af90-bf0c1d2e4f58', 'a0b56c0f-9c4c-4e9e-af90-bf0c1d2e4f54', '8e296a08-7a2a-4c7c-8d7e-9f8a0b1c2d3f', '2023-10-15 09:50:00+00'),
('a0b56c14-9c4c-4e9e-af90-bf0c1d2e4f59', 'a0b56c0f-9c4c-4e9e-af90-bf0c1d2e4f54', '9f496b0a-8b3b-4d8d-9e8f-af9b0c1d3e50', '2023-11-01 16:30:00+00'),
-- Items in Wishlist 7 (User 6 - Mystery Novels)
('b1c67d12-ad5d-4f9f-b0a1-cf1d2e3f5066', 'b1c67d11-ad5d-4f9f-b0a1-cf1d2e3f5065', 'a0b56c0c-9c4c-4e9e-af90-bf0c1d2e4f51', '2023-11-25 15:40:00+00'),
('b1c67d13-ad5d-4f9f-b0a1-cf1d2e3f5067', 'b1c67d11-ad5d-4f9f-b0a1-cf1d2e3f5065', 'b1c67d0e-ad5d-4f9f-b0a1-cf1d2e3f5062', '2023-12-02 11:20:00+00'),
-- Items in Wishlist 8 (User 7 - Science Fiction)
('c2d78e13-be6e-5090-c1b2-d01e3f4f6076', 'c2d78e12-be6e-5090-c1b2-d01e3f4f6075', 'c2d78e10-be6e-5090-c1b2-d01e3f4f6073', '2023-10-05 13:00:00+00'),
('c2d78e14-be6e-5090-c1b2-d01e3f4f6077', 'c2d78e12-be6e-5090-c1b2-d01e3f4f6075', 'd3e89f12-cf7f-5191-d2c3-e12f4f506084', '2023-10-12 15:30:00+00'),
('c2d78e15-be6e-5090-c1b2-d01e3f4f6078', 'c2d78e12-be6e-5090-c1b2-d01e3f4f6075', '550e8401-e29b-41d4-a716-446655440001', '2023-10-25 10:45:00+00'),
-- Items in Wishlist 9 (User 8 - Biography Corner)
('d3e89f16-cf7f-5191-d2c3-e12f4f506088', 'd3e89f15-cf7f-5191-d2c3-e12f4f506087', '6ba7b812-9dad-11d1-80b4-00c04fd430c9', '2023-12-20 10:20:00+00'),
('d3e89f17-cf7f-5191-d2c3-e12f4f506089', 'd3e89f15-cf7f-5191-d2c3-e12f4f506087', '7d793039-a076-4a9e-9a3b-5f6e8f0a6d4c', '2023-12-28 14:35:00+00'),
-- Items in Wishlist 10 (User 9 - Book Club Picks)
('e4f9a018-d080-5292-e3d4-f23450607098', 'e4f9a016-d080-5292-e3d4-f23450607097', '8e296a08-7a2a-4c7c-8d7e-9f8a0b1c2d3f', '2024-01-03 14:30:00+00'),
('e4f9a019-d080-5292-e3d4-f23450607099', 'e4f9a016-d080-5292-e3d4-f23450607097', '9f496b0a-8b3b-4d8d-9e8f-af9b0c1d3e50', '2024-01-08 16:20:00+00'),
('e4f9a01a-d080-5292-e3d4-f2345060709a', 'e4f9a016-d080-5292-e3d4-f23450607097', 'a0b56c0c-9c4c-4e9e-af90-bf0c1d2e4f51', '2024-01-15 11:45:00+00'),
-- Items in Wishlist 11 (User 10 - Professional Dev)
('f5f9a018-d181-5393-f4e5-023560807099', 'f5f9a017-d181-5393-f4e5-023560807098', 'b1c67d0e-ad5d-4f9f-b0a1-cf1d2e3f5062', '2023-11-18 09:40:00+00'),
('f5f9a019-d181-5393-f4e5-02356080709a', 'f5f9a017-d181-5393-f4e5-023560807098', 'c2d78e10-be6e-5090-c1b2-d01e3f4f6073', '2023-11-25 13:15:00+00'),
('f5f9a01a-d181-5393-f4e5-02356080709b', 'f5f9a017-d181-5393-f4e5-023560807098', 'd3e89f12-cf7f-5191-d2c3-e12f4f506084', '2023-12-05 15:50:00+00');

COMMIT;