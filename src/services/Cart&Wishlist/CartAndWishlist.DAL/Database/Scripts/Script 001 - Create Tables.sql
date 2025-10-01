BEGIN;

CREATE TABLE IF NOT EXISTS carts
(
    cart_id    UUID PRIMARY KEY,
    member_id  UUID        NOT NULL,
    created_at TIMESTAMPTZ NOT NULL,
    status     VARCHAR(50) NOT NULL,
    CONSTRAINT chk_cart_status CHECK ( status IN ('Active', 'CheckedOut') )
);

CREATE TABLE IF NOT EXISTS cart_items
(
    cart_item_id UUID PRIMARY KEY,
    cart_id      UUID        NOT NULL,
    book_id      UUID        NOT NULL,
    quantity     INTEGER     NOT NULL,
    added_at     TIMESTAMPTZ NOT NULL,
    CONSTRAINT chk_quantity_positive CHECK ( quantity > 0 ),
    CONSTRAINT fk_cart_item_cart FOREIGN KEY (cart_id) REFERENCES carts (cart_id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS wishlists
(
    wishlist_id UUID PRIMARY KEY,
    member_id   UUID        NOT NULL,
    name        VARCHAR(50) NOT NULL,
    created_at  TIMESTAMPTZ NOT NULL
);

CREATE TABLE IF NOT EXISTS wishlist_items
(
    wishlist_item_id UUID PRIMARY KEY,
    wishlist_id      UUID        NOT NULL,
    book_id          UUID        NOT NULL,
    added_at         TIMESTAMPTZ NOT NULL,
    CONSTRAINT fk_wishlist_item_wishlist FOREIGN KEY (wishlist_id) REFERENCES wishlists (wishlist_id) ON DELETE CASCADE
);

COMMIT;