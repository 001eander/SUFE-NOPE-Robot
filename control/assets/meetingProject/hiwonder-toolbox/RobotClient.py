import os
import socket
import json
import time
import subprocess
import sys

HOST = "192.168.43.203" #  在命令行覆盖
if len(sys.argv) > 1:
    HOST = sys.argv[1]
ADDRESS = (HOST, 8712)
print(ADDRESS)
processes = []


def send_data(client, cmd, **kv):
    global client_id
    jd = {}
    jd['COMMAND'] = cmd  # CONNECT, SEND_DATA
    jd['client_id'] = client_id
    jd['data'] = kv

    jsonstr = json.dumps(jd)
    print('send: ' + jsonstr)
    client.sendall(jsonstr.encode('utf8'))


def handle_recv_msg(msg):
    if msg and type(msg) == str:
        body = json.loads(msg)
        try:
            if body['type'] == 'cmd':
                p = subprocess.Popen(body['data'], shell=True, stdin=subprocess.PIPE, stdout=subprocess.DEVNULL)
                processes.append(p)
            if body['type'] == 'restart':
                print("Client is restarting")
                for p in processes:
                    if p.poll() is None:
                        p.terminate()
                restart_program()
            if body['type'] == 'stop':
                print('Client down')
                for p in processes:
                    if p.poll() is None:
                        p.terminate()
                exit(0)
        except Exception as e:
            print(str(e))
            send_data(client, 'SEND_DATA', Exception=str(e))


def restart_program():
    python = sys.executable
    os.execl(python, python, *sys.argv)


if __name__ == '__main__':
    client_id = str(time.time())
    client = socket.socket()
    client.connect(ADDRESS)
    print(client.recv(1024).decode(encoding='utf8'))
    send_data(client, 'CONNECT')

    while True:
        try:
            msg = client.recv(1024).decode(encoding='utf8')
            handle_recv_msg(msg)
        except KeyboardInterrupt:
            print("\nClient down for interruption")
            exit(0)
