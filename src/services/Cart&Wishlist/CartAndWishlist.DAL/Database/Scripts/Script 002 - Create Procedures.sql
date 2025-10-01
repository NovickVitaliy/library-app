BEGIN;

CREATE OR REPLACE PROCEDURE create_cart_item(p_cart_item_id UUID,
                                  p_cart_id UUID,
                                  p_book_id UUID,
                                  p_quantity INTEGER,
                                  p_added_at TIMESTAMPTZ)
    LANGUAGE plpgsql
AS
$$
BEGIN
    INSERT INTO cart_items (cart_item_id, cart_id, book_id, quantity, added_at)
    VALUES (p_cart_item_id, p_cart_id, p_book_id, p_quantity, p_added_at);
END
$$;

CREATE OR REPLACE PROCEDURE create_cart(p_cart_id UUID,
                             p_member_id UUID,
                             p_created_at TIMESTAMPTZ,
                             p_status VARCHAR(50))
    LANGUAGE plpgsql
AS
$$
BEGIN
    INSERT INTO carts (cart_id, member_id, created_at, status)
    VALUES (p_cart_id, p_member_id, p_created_at, p_status);
END
$$;

CREATE OR REPLACE PROCEDURE add_wishlist(p_wishlist_id UUID,
                              p_member_id UUID,
                              p_name VARCHAR(50),
                              p_created_at TIMESTAMPTZ)
    LANGUAGE plpgsql
AS
$$
BEGIN
    INSERT INTO wishlists (wishlist_id, member_id, name, created_at)
    VALUES (p_wishlist_id, p_member_id, p_name, p_created_at);
END
$$;

CREATE OR REPLACE PROCEDURE add_wishlist_item(p_wishlist_item_id UUID,
                                   p_wishlist_id UUID,
                                   p_book_id UUID,
                                   p_added_at TIMESTAMPTZ)
    LANGUAGE plpgsql
AS
$$
BEGIN
    INSERT INTO wishlist_items (wishlist_item_id, wishlist_id, book_id, added_at)
    VALUES (p_wishlist_item_id, p_wishlist_id, p_book_id, p_added_at);
END
$$;

COMMIT;