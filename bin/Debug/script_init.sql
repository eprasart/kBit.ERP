CREATE TABLE ic_location
(
  id bigserial NOT NULL,
  code text NOT NULL,
  description text,
  address text,
  name text,
  phone text,
  fax text,
  email text,
  note text,
  status text DEFAULT 'A'::text,
  lock_by text,
  lock_at timestamp without time zone,
  insert_by text,
  insert_at timestamp without time zone DEFAULT now(),
  change_by text,
  change_at timestamp without time zone,
  CONSTRAINT ic_location_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);