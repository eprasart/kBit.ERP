﻿--CREATE EXTENSION IF NOT EXISTS pgcrypto SCHEMA public; -- for password encryption

-- Table: ic_location
-- DROP TABLE ic_location;
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
);

-- Table sm_function
-- DROP TABLE sm_function;
CREATE TABLE IF NOT EXISTS sm_function
(
  id bigserial NOT NULL,
  code text NOT NULL,
  description text,
  type text,
  "right" text,
  note text,
  status text DEFAULT 'A',
  insert_by text,
  insert_at timestamp without time zone DEFAULT now(),
  change_by text,
  change_at timestamp without time zone,
  CONSTRAINT sm_function_pkey PRIMARY KEY (id)
);

-- Table: sm_function
-- DROP TABLE sm_function;
CREATE TABLE IF NOT EXISTS sm_function
(
  id bigserial NOT NULL,
  code text NOT NULL,
  description text,
  type text,
  "right" text,
  note text,
  status text,
  insert_by text,
  insert_at timestamp without time zone DEFAULT now(),
  change_by text,
  change_at timestamp without time zone,
  CONSTRAINT sm_function_pkey PRIMARY KEY (id)
);

-- Table: sm_lock
-- DROP TABLE sm_lock;
CREATE TABLE IF NOT EXISTS sm_lock
(
  id bigserial NOT NULL,
  table_name text,
  lock_id bigint NOT NULL,
  ref text,
  lock_by text,
  lock_at timestamp without time zone DEFAULT now(),
  machine_name text,
  machine_username text,
  status text DEFAULT 'A',
  CONSTRAINT sm_lock_pkey PRIMARY KEY (id)
);

-- Table: sm_role
-- DROP TABLE sm_role;
CREATE TABLE IF NOT EXISTS sm_role
(
  id bigserial NOT NULL,
  code text NOT NULL,
  description text,  
  status text DEFAULT 'A',
  insert_by text,
  insert_at timestamp without time zone DEFAULT now(),
  change_by text,
  change_at timestamp without time zone,
  CONSTRAINT sm_role_pkey PRIMARY KEY (id)
);

-- Table: sm_role_function
-- DROP TABLE sm_role_function;
CREATE TABLE IF NOT EXISTS sm_role_function
(
  id bigserial NOT NULL,
  role_code text NOT NULL,
  function_code text NOT NULL,
  "right" text,
  status text DEFAULT 'A',
  insert_by text,
  insert_at timestamp without time zone DEFAULT now(),
  change_by text,
  change_at timestamp without time zone,
  CONSTRAINT sm_role_function_pkey PRIMARY KEY (id)
);

-- Table: sm_session
-- DROP TABLE sm_session;
CREATE TABLE IF NOT EXISTS sm_session
(
  id bigserial NOT NULL,
  username text,
  login_at timestamp without time zone DEFAULT now(),
  logout_at timestamp without time zone,
  version text,
  machine_name text,
  machine_user_name text,
  status text DEFAULT 'A',
  CONSTRAINT sm_session_pkey PRIMARY KEY (id)
);

-- Table: sm_session_log
-- DROP TABLE sm_session_log;
CREATE TABLE IF NOT EXISTS sm_session_log
(
  id bigserial NOT NULL,
  log_at timestamp without time zone DEFAULT now(),
  session_id bigint NOT NULL,
  priority text NOT NULL,
  module text NOT NULL,
  type text NOT NULL,
  message text NOT NULL,
  status text DEFAULT 'A',
  CONSTRAINT sm_session_log_pkey PRIMARY KEY (id)
);

-- Table: sm_user
-- DROP TABLE sm_user;
CREATE TABLE IF NOT EXISTS sm_user
(
  id bigserial NOT NULL,
  username text NOT NULL,
  full_name text,
  pwd text,
  pwd_change_on timestamp without time zone,
  pwd_change_force boolean NOT NULL,
  time_level integer NOT NULL,
  start_on timestamp without time zone,
  end_on timestamp without time zone,
  success integer NOT NULL,
  fail integer NOT NULL,
  locked boolean NOT NULL,
  "right" text,
  security_no text,
  phone text,
  email text,
  note text,
  status text DEFAULT 'A',
  insert_by text,
  insert_at timestamp without time zone DEFAULT now(),
  change_by text,
  change_at timestamp without time zone,
  CONSTRAINT sm_user_pkey PRIMARY KEY (id)
);

-- Table: sm_user_function
-- DROP TABLE sm_user_function;
CREATE TABLE IF NOT EXISTS sm_user_function
(
  id bigserial NOT NULL,
  username bigint NOT NULL,
  function_code bigint NOT NULL,
  status text DEFAULT 'A',
  insert_by text,
  insert_at timestamp without time zone DEFAULT now(),
  change_by text,
  change_at timestamp without time zone,
  CONSTRAINT sm_user_function_pkey PRIMARY KEY (id)
);

-- Table: sm_user_role
-- DROP TABLE sm_user_role;
CREATE TABLE IF NOT EXISTS sm_user_role
(
  id bigserial NOT NULL,
  user_id bigint NOT NULL,
  role_id bigint NOT NULL,
  status text DEFAULT 'A',
  insert_by text,
  insert_at timestamp without time zone DEFAULT now(),
  change_by text,
  change_at timestamp without time zone,
  CONSTRAINT sm_user_role_pkey PRIMARY KEY (id)
);

-- Table: sys_branch
-- DROP TABLE sy_branch;
CREATE TABLE IF NOT EXISTS sy_branch
(
  id bigserial NOT NULL,
  code text NOT NULL,
  description text,
  note text,
  status text default 'A',
  insert_by text,
  insert_at timestamp without time zone DEFAULT now(),
  change_by text,
  change_at timestamp without time zone,
  CONSTRAINT sy_branch_pkey PRIMARY KEY (id)
);

insert into sy_branch (code, description) select '000', 'Head Office' where not exists (select id from sy_branch);

-- Table: sys_error_log
-- DROP TABLE sy_error_log;
CREATE TABLE IF NOT EXISTS sy_error_log
(
  id bigserial NOT NULL,
  session_id bigint NOT NULL,
  at timestamp without time zone DEFAULT now(),
  message text,
  trace text,
  info text,
  status text DEFAULT 'A',
  CONSTRAINT sy_error_log_pkey PRIMARY KEY (id)
);

-- Table: sys_config
-- DROP TABLE sy_config;
CREATE TABLE IF NOT EXISTS sy_config
(
  id bigserial NOT NULL,
  name text NOT NULL,
  value text,
  note text,
  status text default 'A',
  CONSTRAINT sy_config_pkey PRIMARY KEY (id)
);