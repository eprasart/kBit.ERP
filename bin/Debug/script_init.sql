-- ic_location
CREATE TABLE IF NOT EXISTS ic_location
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
  insert_by text,
  insert_at timestamp without time zone DEFAULT now(),
  change_by text,
  change_at timestamp without time zone,
  CONSTRAINT ic_location_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);

-- sm_lock
CREATE TABLE IF NOT EXISTS sm_lock
(
  id bigserial NOT NULL,
  table_name text,
  username text,
  lock_id bigint NOT NULL,
  lock_at timestamp without time zone DEFAULT now(),
  CONSTRAINT sm_lock_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);

-- Table: sm_function
REATE TABLE sm_function
(
  id bigserial NOT NULL,
  name text NOT NULL,
  code text,
  type text,
  "right" text,
  note text,
  status text,
  lock_by text,
  lock_at timestamp without time zone,
  insert_by text,
  insert_at timestamp without time zone DEFAULT now(),
  change_by text,
  change_at timestamp without time zone,
  CONSTRAINT sm_function_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);