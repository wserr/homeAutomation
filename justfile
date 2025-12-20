copy-systemd-services:
	sudo cp ./systemd/recurring-tasks/*.service /etc/systemd/system && \
	sudo cp ./systemd/recurring-tasks/*.timer /etc/systemd/system && \
	sudo systemctl daemon-reload

backup-all:
	find ./systemd/recurring-tasks -type f -name "*.service" -exec bash -c 'sudo systemctl restart "$(basename "$1")"' _ {} \;

status-services:
	find ./systemd/recurring-tasks -type f -name "*.service" -exec bash -c 'sudo systemctl status "$(basename "$1")"' _ {} \;

status-timers:
	find ./systemd/recurring-tasks -type f -name "*.timer" -exec bash -c 'sudo systemctl status "$(basename "$1")"' _ {} \;
