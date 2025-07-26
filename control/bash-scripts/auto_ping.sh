ip_mask="192.168.43"
start=2
end=99

for i in $(seq $start $end)
 do
  ping -c 1 -t 1 $ip_mask.$i > /dev/null 2>&1
    if [ $? -eq 0 ]
      then
        echo "ip $ip_mask.$i ok"
    fi
 done
