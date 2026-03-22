CREATE TABLE IF NOT EXISTS orders (
    id SERIAL PRIMARY KEY,
    customer TEXT NOT NULL,
    product TEXT NOT NULL,
    quantity INT NOT NULL DEFAULT 1,
    total_amount DECIMAL(10,2) NOT NULL,
    status TEXT NOT NULL DEFAULT 'pending',
    created_at TIMESTAMPTZ NOT NULL DEFAULT now(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT now()
);

INSERT INTO orders (customer, product, quantity, total_amount, status) VALUES
    ('alice', 'Widget A', 2, 49.98, 'pending'),
    ('bob', 'Widget B', 1, 24.99, 'confirmed'),
    ('carol', 'Widget C', 5, 124.95, 'shipped');
