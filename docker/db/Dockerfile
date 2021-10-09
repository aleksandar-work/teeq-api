# We choose exact tag (not 'latest'), to be sure that new version wont break creating image
FROM mcr.microsoft.com/mssql/server:2017-CU17-ubuntu

# Create app directory
RUN mkdir -p /usr/src/db
WORKDIR /usr/src/db

# Copy initialization scripts
COPY . /usr/src/db

# Grant permissions for the run-initialization script to be executable
RUN chmod +x /usr/src/db/run-initialization.sh

# Run Microsoft SQl Server and initialization script (at the same time)
# Note: If you want to start MsSQL only (without initialization script) you can comment bellow line out, CMD entry from base image will be taken
CMD /bin/bash ./entrypoint.sh