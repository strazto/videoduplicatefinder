---
name: Feature request
about: Suggest an idea for this project
title: '[OS.Client]Feature: '
labels: ''
assignees: ''

x-required: &required
  validations:
    required: true

body:
  - type: dropdown
    attributes:
      label: Operating System
      description: What OS are you running?
      options:
        - Windows
        - OSX Mac
        - Linux
        - Other?
  - type: dropdown
    <<: *required
    
    attributes:      
      label: Client Type
      description: Which client are you trying to use?
      options:
        - Windows only GUI (Only in versions <2.1.0)
        - Cross platform GUI (Versions >= 2.1.0)
        - Console
        - Web
  - type: dropdown
    <<: *required
    
    attributes:
      label: videoduplicatefinder version
      description: |
        What is the version number of the release you're using?
      
---

**Environment**
 - OS: [e.g. Windows / Linux / Any]
 - Client: [e.g. Windows, Cross-Platform, Console, Web]
 - Version: [The version number of the release you downloaded]
**Describe the solution you'd like**
A clear and concise description of what you want to happen.
