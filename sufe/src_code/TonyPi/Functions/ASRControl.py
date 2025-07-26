#!/usr/bin/python3
# coding=utf8
import sys
sys.path.append('/home/pi/TonyPi/')
import time
from LABConfig import *
from ActionGroupDict import *
import HiwonderSDK.TTS as TTS
import HiwonderSDK.ASR as ASR
import HiwonderSDK.Board as Board
import HiwonderSDK.ActionGroupControl as AGC

# 语音控制

try:
    asr = ASR.ASR()
    tts = TTS.TTS()

    debug = True

    if debug:
        asr.eraseWords()
        asr.setMode(2)
        asr.addWords(1, 'kai shi')
        asr.addWords(2, 'wang qian zou')
        asr.addWords(2, 'qian jin')
        asr.addWords(2, 'zhi zou')
        asr.addWords(3, 'wang hou tui')
        asr.addWords(4, 'xiang zuo yi')
        asr.addWords(5, 'xiang you yi')

    data = asr.getResult()
    Board.setPWMServoPulse(1, 1500, 500)
    Board.setPWMServoPulse(2, servo2, 500)    
    AGC.runActionGroup('stand')
    action_finish = True
    tts.TTSModuleSpeak('[h0][v10][m3]', '准备就绪')
    print('''口令：kai shi
指令2：wang qian zou
指令2：qian jin
指令2：zhi zou
指令3：wang hou tui
指令4：xiang zuo yi
指令5：xiang you yi''')
    time.sleep(2)
except:
    print('传感器初始化出错')

while True:
    data = asr.getResult()
    if data:
        print('result:', data)
        tts.TTSModuleSpeak('', '收到')
        time.sleep(1)
        AGC.runActionGroup(action_group_dict[str(data - 1)], 2 ,True)
    time.sleep(0.01)
