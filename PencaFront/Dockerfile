# Use an official Node runtime as the base
FROM node:22-alpine

# Set the working directory inside the container
WORKDIR /usr/src/app

# Copy package.json and yarn.lock to the container
COPY package*.json yarn.lock ./

# Install dependencies using Yarn
RUN yarn install

# Copy the rest of the application code to the container
COPY . .

# Build the Angular app
RUN npm run build

# Expose the port where the app will run (adjust as needed)
EXPOSE 4200

# Start the Angular app
CMD ["npm", "run", "dockerstart"]
