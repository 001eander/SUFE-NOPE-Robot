import socket
from threading import Thread
import time
import json
import sys
import atexit

HOST = "192.168.1.100"
if len(sys.argv) > 1:
    HOST = sys.argv[1]
ADDRESS = (HOST, 8712)
print(ADDRESS)
g_socket_server = None
g_conn_pool = {}


def init():
    global g_socket_server
    g_socket_server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    g_socket_server.bind(ADDRESS)
    g_socket_server.listen(5)
    print("server start, wait for client connecting...")


def accept_client():
    while True:
        client, info = g_socket_server.accept()
        thread = Thread(target=message_handle, args=(client, info))
        thread.setDaemon(True)
        thread.start()


def message_handle(client, info):
    client.sendall("connect server successfully!".encode(encoding='utf8'))
    client_id = None
    while True:
        try:
            bytes = client.recv(1024)
            msg = bytes.decode(encoding='utf8')
            jd = json.loads(msg)
            cmd = jd['COMMAND']
            client_id = jd['client_id']
            if 'CONNECT' == cmd:
                g_conn_pool[client_id] = client
                print('\n on client connect: ' + client_id, info)
            elif 'SEND_DATA' == cmd:
                print('\n recv client msg: ' + client_id, jd['data'])
        except Exception as e:
            print(e)
            if client_id:
                remove_client(client_id)
            break


def remove_client(client_type):
    client = g_conn_pool[client_type]
    if client:
        client.close()
        g_conn_pool.pop(client_type)
        print("client offline: " + client_type)


@atexit.register
def exitToClear():
    global g_conn_pool
    need_remove = [k for k in g_conn_pool.keys()]
    for k in need_remove:
        try:
            send_msg('stop', g_conn_pool[k])
        except:
            pass


def send_msg(msg: str, client):
    if msg:
        msg_type = msg.split(' ')[0].lower()
        if msg_type in ['cmd', 'restart', 'stop']:
            body = {
                'type': msg_type,
                'data': ' '.join(msg.split(' ')[1:]).strip()
            }
            msg = json.dumps(body).encode(encoding='utf8')
            client.sendall(msg)


if __name__ == '__main__':
    init()
    thread = Thread(target=accept_client)
    thread.setDaemon(True)
    thread.start()
    while True:
        try:
            send_command = input("\n[Send To Clients]:")
            for k, client in g_conn_pool.items():
                send_msg(send_command, client)
        except KeyboardInterrupt:
            print("\nServer down")
            exit(0)
