#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <fcntl.h>
#include <termios.h>
#include <pthread.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <sys/stat.h>

// Function to receive serial communication from Linux and transfer it to the socket
void* serial_to_socket() {
    int ser_fd;
    int serv_fd;
    struct sockaddr_in serial_addr;
    int addrlen2 = sizeof(serial_addr);
    int newsocket2;
    
    ser_fd = open("/dev/ttyS0", O_RDWR | O_NOCTTY | O_SYNC);
    if (ser_fd < 0) {
        fprintf(stderr, "Failed to open serial port\n");
    }
    
     struct termios tty;
    // Configure serial port
    tcgetattr(ser_fd, &tty);
    cfsetospeed(&tty, B9600);
    cfmakeraw(&tty);
    tcsetattr(ser_fd, TCSANOW, &tty);
    char buf[1024];
    int len;
    
    
    serv_fd = socket(AF_INET, SOCK_STREAM, 0);
    if (serv_fd < 0) {
        fprintf(stderr, "Failed to create socket\n");
    }
    memset(&serial_addr, 0, sizeof(serial_addr));
    serial_addr.sin_family = AF_INET;
    serial_addr.sin_addr.s_addr = htonl(INADDR_ANY);
    serial_addr.sin_port = htons(8000);
    
    if (bind(serv_fd, (struct sockaddr *)&serial_addr, sizeof(serial_addr)) < 0) {
        perror("bind failed");
        exit(EXIT_FAILURE);
    }
    
    if (listen(serv_fd, 3) < 0) {
        perror("listen");
        exit(EXIT_FAILURE);
    }
    printf("waiting...\n");
    // 들어오면 연결하기
    if ((newsocket2 = accept(serv_fd, (struct sockaddr *)&serial_addr, (socklen_t *)&addrlen2)) < 0) {
        perror("accept");
        exit(EXIT_FAILURE);
    } 
    printf("connect\n");


    while (1) {
        len = read(ser_fd, buf, sizeof(buf));
        if (len > 0) {
            switch (buf[0]) {
            case 'r':
                printf("반복문 20\n");
                send(newsocket2, "r", 1, 0);
                break;
            case 'g':
                printf("반복문  30\n");
                send(newsocket2, "g", 1, 0);
                break;
            case 'b':
                printf("반복문  40\n");
                send(newsocket2, "b", 1, 0);
                break;
            case 'o':
                printf("반복문 50\n");
                send(newsocket2, "o", 1, 0);
                break;
            default:
                printf("반복문  60\n");
                send(newsocket2, "a", 1, 0);
                break;
            }
            printf("Received: %c\n", buf[0]);
        }
    }
    close(serv_fd);
}

// Function to transfer image files to the socket using threads
void* transfer_images() {
    int sock_fd;
   struct sockaddr_in server_addr;
   int newsocket;
    int addrlen = sizeof(server_addr);
    sock_fd = socket(AF_INET, SOCK_STREAM, 0);
    if (sock_fd < 0) {
        fprintf(stderr, "Failed to create socket\n");
    }
    memset(&server_addr, 0, sizeof(server_addr));
    server_addr.sin_family = AF_INET;
    server_addr.sin_addr.s_addr = htonl(INADDR_ANY);
    server_addr.sin_port = htons(9000);
     if (bind(sock_fd, (struct sockaddr *)&server_addr, sizeof(server_addr)) < 0) {
        perror("bind failed");
        exit(EXIT_FAILURE);
    }
     printf("waiting...\n");
    // 연결올때까지 기다리기
    if(listen(sock_fd, 3) < 0) {
        perror("listen");
        exit(EXIT_FAILURE);
    }
         // 들어오면 연결하기
    if((newsocket = accept(sock_fd, (struct sockaddr *)&server_addr, (socklen_t *)&addrlen)) < 0) {
        perror("accept");
        exit(EXIT_FAILURE);
    } 
    
    char filename[32];
    int z = 0;
    //char buf[1024];
    for (int j = 0; j < 600; j++)
    {
        if (z = 0)
        {
            for (int i = 1; i <= 60; i++) {
                // Load image file
                snprintf(filename, sizeof(filename), "baseball%d.bmp", i);
                FILE* fd = fopen(filename, "r");
                if (fd == NULL) {
                    perror("fopen");
                    exit(EXIT_FAILURE);
                }
                struct stat file;
                stat(filename, &file);
                int size = file.st_size;
                send(newsocket, &size, sizeof(int), 0);

                fseek(fd, 0, SEEK_END);
                long file_size = ftell(fd);
                rewind(fd);

                char* file_buffer = (char*)malloc(file_size * sizeof(char));
                if (file_buffer == NULL) {
                    perror("malloc");
                    exit(EXIT_FAILURE);
                }
                printf("반복문 13\n");

                size_t result = fread(file_buffer, 1, file_size, fd);
                if (result != file_size) {
                    perror("fread");
                    exit(EXIT_FAILURE);
                }
                int bytes_sent = send(newsocket, file_buffer, file_size, 0);
                if (bytes_sent < 0) {
                    perror("send");
                    exit(EXIT_FAILURE);
                }

                free(file_buffer);
                fclose(fd);
                printf("반복 2\n");

                z = 1;
            }
        }
        else {
            for (int i = 60; i >= 1; i--) {
                // Load image file
                snprintf(filename, sizeof(filename), "baseball%d.bmp", i);
                FILE* fd = fopen(filename, "r");
                if (fd == NULL) {
                    perror("fopen");
                    exit(EXIT_FAILURE);
                }
                struct stat file;
                stat(filename, &file);
                int size = file.st_size;
                send(newsocket, &size, sizeof(int), 0);

                fseek(fd, 0, SEEK_END);
                long file_size = ftell(fd);
                rewind(fd);

                char* file_buffer = (char*)malloc(file_size * sizeof(char));
                if (file_buffer == NULL) {
                    perror("malloc");
                    exit(EXIT_FAILURE);
                }
                printf("반복문 13\n");

                size_t result = fread(file_buffer, 1, file_size, fd);
                if (result != file_size) {
                    perror("fread");
                    exit(EXIT_FAILURE);
                }
                int bytes_sent = send(newsocket, file_buffer, file_size, 0);
                if (bytes_sent < 0) {
                    perror("send");
                    exit(EXIT_FAILURE);
                }

                free(file_buffer);
                fclose(fd);
                printf("반복 2\n");

                z = 0;
            }
        }
        }
      
    }
    close(sock_fd);
    pthread_exit(NULL);
}

// Main function
int main() {
    pthread_t thread1, thread2;
    // Start thread to receive serial communication and transfer it to the socket
    if (pthread_create(&thread1, NULL, serial_to_socket, NULL) != 0) {
        fprintf(stderr, "Failed to create thread\n");
        return -1;
    }
    // Start thread to transfer image files to the socket using threads
    if (pthread_create(&thread2, NULL, transfer_images, NULL) != 0) {
        fprintf(stderr, "Failed to create thread\n");
        return -1;
    }
    // Wait for threads to finish
    pthread_join(thread1, NULL);
    pthread_join(thread2, NULL);
    // Close serial port and socket
    return 0;
}
