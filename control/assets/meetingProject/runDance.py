#!/usr/bin/env python3
# encoding: utf-8
import sys

sys.path.append('/home/pi/TonyPi/')
import HiwonderSDK.Board as Board
import HiwonderSDK.ActionGroupControl as AGC
import time
import os

if __name__ == "__main__":
    name_dict = {"0": "dancer_sing", "1": "squat", "2": "dreamer_sing", "3": "bow", "stand":"stand"}

    if len(sys.argv) > 1:
        key = sys.argv[1]
        name = name_dict[key]
        AGC.runActionGroup(name)
