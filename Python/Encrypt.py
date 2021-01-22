#coding:utf-8
#References:https://github.com/evi1ox/MSSQL_BackDoor/blob/master/tools/py_bin_encrypt/EncodeShellcode.py

import sys
import base64
import argparse


def xor(data, key):
    l = len(key)
    keyAsInt = list(map(ord, key))
    return bytes(bytearray((
        (data[i] ^ keyAsInt[i % l]) for i in range(0,len(data))
    )))

if __name__ == '__main__':
    parser = argparse.ArgumentParser(description="python3 {0} -f payload.bin -k Evi1oX".format(sys.argv[0]))
    parser.add_argument("-f","--file", help="Raw Shellcode File",required=True)
    parser.add_argument("-k","--key", help="XOR Encrypted key",required=True)
    args = parser.parse_args()

    try:
        with open(args.file, 'rb') as f:
            scBytes = f.read()
            xorBytes = xor(scBytes, args.key)
            print("XorKey: "+args.key)
            print("Result: "+base64.b64encode(xorBytes).decode())
    except Exception as e:
        print(e)
        sys.exit()
