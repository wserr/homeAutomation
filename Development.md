# Development setup

## Install kubectl on client machine

See [installation tutorial here](https://kubernetes.io/docs/tasks/tools/install-kubectl-linux/)

## Install helm on client machine

See [installation tutorial here](https://helm.sh/docs/intro/install/)

## Configure cluster with kubectl

- Get cluster info with command `microk8s config`
- Add the cluster info to the kubectl config file on the client machine at `~/.kube/config`

Example config file

```bash
apiVersion: v1 
clusters: 
- cluster:  
    certificate-authority-data: 
    ......
      server: https://63.250.54.81:16443  
  name: homeserver  
contexts:  
- context:  
    cluster: homeserver 
    user: admin  
  name: homeserver 
current-context: homeserver 
kind: Config  
preferences: {}  
users:  
- name: admin  
  user:  
    token: TnNDNURIV1I3RFdFMFora1NEaTdlNi9uNEgwR0pHRVRVajdPeFFrYWUvND0K  
```

- In config file above, change context name
- To use this cluster with kubectl, enter `kubectl config use-context homeserver`

## Configure cluster with helm

- Run `helm --kube-context homeserver`
