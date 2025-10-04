copy-systemd-services:
	cp ./systemd/recurring-tasks/*.service /etc/systemd/system && \
	cp ./systemd/recurring-tasks/*.timer /etc/systemd/system && \
	sudo systemctl daemon-reload
