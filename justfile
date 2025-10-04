copy-systemd-services:
	sudo cp ./systemd/recurring-tasks/*.service /etc/systemd/system && \
	sudo cp ./systemd/recurring-tasks/*.timer /etc/systemd/system && \
	sudo systemctl daemon-reload
