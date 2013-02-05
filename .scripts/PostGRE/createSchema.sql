CREATE TABLE "Subscription"
(
  "SubscriptionId" integer NOT NULL,
  "Name" text NOT NULL,
  "BillingType" smallint NOT NULL,
  CONSTRAINT "Subscription_PK" PRIMARY KEY ("SubscriptionId")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "Subscription"
  OWNER TO postgres;

-- Table: "Country"

-- DROP TABLE "Country";

CREATE TABLE "Country"
(
  "CountryId" integer NOT NULL,
  "Code" character varying(2) NOT NULL,
  "Name" text NOT NULL,
  "NativeName" text,
  "System" smallint NOT NULL DEFAULT 0,
  CONSTRAINT "Country_PK" PRIMARY KEY ("CountryId")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "Country"
  OWNER TO postgres;
