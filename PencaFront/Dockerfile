# Use the official Node.js image as the base
FROM node:22-alpine

# Set the working directory inside the container
WORKDIR /usr/src/app

# Copy package.json and package-lock.json to the container
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code to the container
COPY . .

# Build the Angular app
RUN npm run build

# Expose the port where the app will run (adjust as needed)
EXPOSE 4200

# Start the Angular app
CMD ["npm", "run", "dockerstart"]
