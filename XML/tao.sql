CREATE USER hisbvnhitp0617 IDENTIFIED BY hisbvnhitp0617 DEFAULT TABLESPACE his QUOTA 100M ON his;
GRANT CREATE SESSION, IMP_FULL_DATABASE TO hisbvnhitp0617;
GRANT JAVASYSPRIV, JAVAUSERPRIV TO hisbvnhitp0617;
GRANT ALL PRIVILEGES TO hisbvnhitp0617;
ALTER PROFILE DEFAULT LIMIT PASSWORD_LIFE_TIME UNLIMITED;
commit;
exit;